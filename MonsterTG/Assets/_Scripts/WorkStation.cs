using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : MonoBehaviour {

    public float WorkTimer;
    public float WorkRate;
    public float GrowthRate;
    public int GrowthAmount;

    protected bool isOccupied = false;
    protected CreatueBehavior occupant;

    int expCount = 0;

    public void Start()
    {
        WorkTimer = Time.time;
    }

    public void SetCreature(CreatueBehavior creature)
    {
        isOccupied = true;
        occupant = creature;

        creature.isStationed = true;
        creature.currentStation = this;
    }

    public void RemoveCreature()
    {
        occupant.isStationed = false;
        occupant.currentStation = null;

        isOccupied = false;
        occupant = null;
    }

    protected virtual void Update()
    {
        if (occupant == null)
        {
            return;
        }   

        if (Time.time - WorkTimer > WorkRate)
        {
            WorkTimer = Time.time;

            Work();
        }

    }

    protected virtual void Work()
    {
        occupant.stats.Hunger -= 1;
        occupant.stats.Happiness -= 1;

        occupant.stats.Hunger = Mathf.Clamp(occupant.stats.Hunger, 0, 20);
        occupant.stats.Happiness = Mathf.Clamp(occupant.stats.Happiness, 0, 20);
        expCount += 1;
        if (expCount > 5)
        {
            expCount = 0;
            occupant.stats.Level += 1;
        }

        GameController.Instance.Save();
        GameController.Instance.UpdatePanel(occupant);
    }

    private void OnMouseUp()
    {
        if (GameController.Instance.selected != null)
        {
            Vector3 newPos = new Vector3(transform.position.x, GameController.Instance.selected.transform.parent.position.y, transform.position.z);
            GameController.Instance.selected.transform.parent.position = newPos;
            SetCreature(GameController.Instance.selected);
        }
    }
}
