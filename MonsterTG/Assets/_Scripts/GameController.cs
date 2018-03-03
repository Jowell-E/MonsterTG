using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    public UIController MainCanvas;
    public List<CreatueBehavior> Creatures = new List<CreatueBehavior>();
    public List<WorkStation> Stations = new List<WorkStation>();
    public static Vector2 PiecePlacementOffset = new Vector2(3, 3);

    public GameObject creaturePrefab;
    public Transform creaturesParent;
    public GameObject mainHouse;

    public CreatueBehavior selected;

    public int Money;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }
    private static GameController instance = null;

    void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        Load();

        if (Creatures.Count == 0)
        {
            GameObject newCreature = Instantiate(creaturePrefab, creaturesParent);
            Creatures.Add(newCreature.GetComponentInChildren<CreatueBehavior>());
            Creatures[0].stats = new Stats("Steve", 0, 0, 0, 0, 0, 20, 20);
            Creatures[0].LoadCreature(Creatures[0].stats);
        }
    }

    public void RemoveFromStation(GameObject creature)
    {
        CreatueBehavior behavior = creature.GetComponentInChildren<CreatueBehavior>();
        if (behavior.isStationed)
        {
            behavior.currentStation.RemoveCreature();
        }
    }

    public void CheckForStation(GameObject creature)
    {
        bool isStation = false;
        WorkStation station = null;
        foreach (var solution in Stations)
        {
            if (((creature.transform.localPosition.x > solution.transform.localPosition.x - PiecePlacementOffset.x) && (creature.transform.localPosition.x < solution.transform.localPosition.x + PiecePlacementOffset.x))
                && ((creature.transform.localPosition.z > solution.transform.localPosition.z - PiecePlacementOffset.y) && (creature.transform.localPosition.z < solution.transform.localPosition.z + PiecePlacementOffset.y)))
            {

                isStation = true;

                station = solution as WorkStation;

                if (isStation)
                {
                    break;
                }
            }
        }

        if (station != null && isStation)
        {
            Vector3 newPos = new Vector3(station.transform.position.x, creature.transform.position.y, station.transform.position.z);
            creature.transform.position = newPos;
            station.SetCreature(creature.GetComponentInChildren<CreatueBehavior>());
            selected = null;
        }
    }

    public void CheckForCreature(GameObject fruit)
    {
        bool isCreature = false;
        CreatueBehavior creature = null;
        foreach (var solution in Creatures)
        {
            if (((fruit.transform.localPosition.x > solution.transform.parent.localPosition.x - PiecePlacementOffset.x) && (fruit.transform.localPosition.x < solution.transform.parent.localPosition.x + PiecePlacementOffset.x))
                && ((fruit.transform.localPosition.z > solution.transform.parent.localPosition.z - PiecePlacementOffset.y) && (fruit.transform.localPosition.z < solution.transform.parent.localPosition.z + PiecePlacementOffset.y)))
            {

                isCreature = true;

                creature = solution as CreatueBehavior;

                if (isCreature)
                {
                    break;
                }
            }
        }

        if (creature != null && isCreature)
        {
            Vector3 newPos = new Vector3(creature.transform.parent.position.x, fruit.transform.position.y, creature.transform.parent.position.z);
            fruit.transform.position = newPos;
            creature.Eat(fruit.GetComponent<FruitBehavior>());
        }
    }

    public void ShowStatsPanel(CreatueBehavior creatue)
    {
        MainCanvas.StatsPanel(creatue);
    }

    public void HidePanels()
    {
        MainCanvas.HidePanels();
    }

    public void ShowShopPanel()
    {
        MainCanvas.ShopPanel();
    }

    public void UpdatePanel(CreatueBehavior creature)
    {
        MainCanvas.UpdatePanel(creature);
    }

    public void UpdateWallet()
    {
        MainCanvas.UpdateMoney(Money);
    }

    public void Run()
    {
        int gain = 10;
        Stats stats = selected.stats;

        gain += stats.Strength;
        gain += stats.Agility;
        gain += stats.Endurance;
        gain -= stats.Happiness;
        gain -= stats.Hunger;

        MainCanvas.Award(stats.Name, gain);
        Money += gain;
        UpdateWallet();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.sav");

        PlayerData data = new PlayerData();

        for (int i = 0; i < Creatures.Count; i++)
        {
            data.creatures.Add(Creatures[i].stats);
        }

        data.Money = Money;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/Save.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Save.sav", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            foreach (Stats creature in data.creatures)
            {
                GameObject newCreature = Instantiate(creaturePrefab, creaturesParent);

                newCreature.GetComponentInChildren<CreatueBehavior>().LoadCreature(creature);

                Creatures.Add(newCreature.GetComponentInChildren<CreatueBehavior>());
            }

            Money = data.Money;
            
            file.Close();
        }

        UpdateWallet();

    }

    public void ClearSave()
    {
        Creatures.Clear();

        GameObject newCreature = Instantiate(creaturePrefab, creaturesParent);
        Creatures.Add(newCreature.GetComponentInChildren<CreatueBehavior>());
        Creatures[0].stats = new Stats("Steve", 0, 0, 0, 0, 0, 0, 0);

        Money = 0;

        Save();
        Application.Quit();
    }
}

[Serializable]
class PlayerData
{
    public List<Stats> creatures = new List<Stats>();

    public int Money;
}