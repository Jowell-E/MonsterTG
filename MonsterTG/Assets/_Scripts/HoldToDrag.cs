using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToDrag : MonoBehaviour {


    public float holdLimit = 1f;
    private float dragTimer;
    private bool holding = false;

    CreatueBehavior creature;
    public LayerMask layer;
	// Use this for initialization
	void Start () {
        creature = GetComponentInChildren<CreatueBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if (holding)
        {
            if (Time.time - dragTimer > holdLimit)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layer))
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Vector3 newPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                        transform.position = newPos;
                    }
                }
            }
        }
	}

    private void OnMouseDown()
    {
        if (creature.eating)
        {
            return;
        }

        dragTimer = Time.time;
        holding = true;
        Camera.main.GetComponent<DragToMove>().disabled = true;

        GameController.Instance.RemoveFromStation(this.gameObject);
        GameController.Instance.selected = creature;
    }

    private void OnMouseExit()
    {
        if (creature.eating)
        {
            return;
        }

        if (Time.time - dragTimer < holdLimit)
        {
            holding = false;
            Camera.main.GetComponent<DragToMove>().disabled = false;
        }
    }

    private void OnMouseUp()
    {
        if (creature.eating)
        {
            return;
        }

        if (Time.time - dragTimer > .05f && Time.time - dragTimer < holdLimit)
        {
            GameController.Instance.ShowStatsPanel(creature);
        }

        holding = false;
        Camera.main.GetComponent<DragToMove>().disabled = false;

        GameController.Instance.CheckForStation(this.gameObject);
    }
}
