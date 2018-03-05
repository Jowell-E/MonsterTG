using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Item
{
    public string Name;
    public int ID;
    public int Cost;

    public Item(string name, int id, int cost)
    {
        this.Name = name;
        this.ID = id;
        this.Cost = cost;
    }
}

public class ItemShop : MonoBehaviour {

    public List<Item> Shop = new List<Item>();

    public GameObject fruit1;
    public GameObject fruit2;
    public GameObject protein;
    public GameObject newSlime;


    public void Buy(int itemID)
    {
        Item selected = null;
        foreach(Item item in Shop)
        {
            if (itemID == item.ID)
            {
                selected = item;
                break;
            }
        }

        if (GameController.Instance.Money < selected.Cost)
        {
            return;
        }

        GameController.Instance.Money -= selected.Cost;
        GameController.Instance.UpdateWallet();
        Spawn(selected);
    }

    void Spawn(Item selected)
    {
        if (selected.ID == 0)
        {
            Instantiate(fruit1, new Vector3(0, 5, 0), fruit1.transform.rotation);
        }else if (selected.ID == 1)
        {
            Instantiate(fruit2, new Vector3(0, 5, 0), fruit2.transform.rotation);
        }
        else if (selected.ID == 2)
        {
            GameObject newCreature = Instantiate(newSlime, new Vector3(0, 5, 0), newSlime.transform.rotation);
            GameController.Instance.Creatures.Add(newCreature.GetComponentInChildren<CreatueBehavior>());
            GameController.Instance.Creatures[GameController.Instance.Creatures.Count - 1].stats = new Stats("Steve", 0, 0, 0, 0, 0, 20, 20);
            GameController.Instance.Creatures[GameController.Instance.Creatures.Count - 1].LoadCreature(GameController.Instance.Creatures[GameController.Instance.Creatures.Count - 1].stats);
            GameController.Instance.Save();
        }
        else if (selected.ID == 3)
        {
            Instantiate(protein, new Vector3(0, 5, 0), protein.transform.rotation);
        }
    }
}
