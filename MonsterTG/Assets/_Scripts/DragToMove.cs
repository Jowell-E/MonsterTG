using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove : MonoBehaviour {

    public float dragSpeed = 1f;

    public float minDistance = 10;
    public float maxDistance = 20;

    public bool disabled = false;

	void Update () {
        if (disabled)
        {
            return;
        }


        if (Input.touchCount == 1)
        {
            Camera camera = Camera.main;

            Touch touchZero = Input.GetTouch(0);

            Vector3 newPos = transform.position;

            float xPos = Mathf.Clamp((touchZero.deltaPosition.x - touchZero.deltaPosition.y)  * dragSpeed * Time.deltaTime + newPos.x, minDistance, maxDistance);

            float zPos = Mathf.Clamp((touchZero.deltaPosition.x + touchZero.deltaPosition.y) * dragSpeed * Time.deltaTime + newPos.z, -maxDistance, -minDistance);
            
            newPos = new Vector3(xPos, newPos.y, zPos);
            
            transform.position = newPos;
        }
    }
}
