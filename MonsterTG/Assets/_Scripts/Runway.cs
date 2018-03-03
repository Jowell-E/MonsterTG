using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runway : MonoBehaviour {


    private void OnMouseUp()
    {
        if (GameController.Instance.selected != null)
        {
            GameController.Instance.Run();
        }
    }
}
