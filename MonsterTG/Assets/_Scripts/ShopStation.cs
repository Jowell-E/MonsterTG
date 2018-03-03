using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStation : MonoBehaviour {

    void OnMouseDown()
    {
        GameController.Instance.ShowShopPanel();
    }
}
