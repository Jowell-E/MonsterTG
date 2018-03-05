using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour {
    public int HungerAmount;
    public int EnduranceAmount;
    public int StrengthAmount;
    public int AgilityAmount;
    public int HappinessAmount;

    public float holdLimit = 1f;
    private float dragTimer;
    private bool holding = false;

    public TreeBehavior tree;
    public LayerMask layer;
    
    void Update()
    {
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
        if (tree != null)
        {
            tree.Fruits.Remove(this);
            tree.Invoke("SpawnFruit", tree.spawnTime);
            tree = null;
        }

        dragTimer = Time.time;
        holding = true;
        Camera.main.GetComponent<DragToMove>().disabled = true;
    }

    private void OnMouseUp()
    {
        holding = false;
        Camera.main.GetComponent<DragToMove>().disabled = false;

        GameController.Instance.CheckForCreature(this.gameObject);
    }
}
