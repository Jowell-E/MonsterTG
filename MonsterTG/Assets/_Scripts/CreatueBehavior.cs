using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Stats
{
    public string Name;
    public int Type;
    public int Level;
    public int Strength;
    public int Agility;
    public int Endurance;
    public int Hunger;
    public int Happiness;

    public Stats(string name, int type, int lvl, int str, int agi, int end, int hun, int hap)
    {
        this.Name = name;
        this.Type = type;
        this.Level = lvl;
        this.Strength = str;
        this.Agility = agi;
        this.Endurance = end;
        this.Hunger = hun;
        this.Happiness = hap;
    }
}

public class CreatueBehavior : MonoBehaviour {

    public Stats stats;
    public bool isStationed = false;
    public WorkStation currentStation;
    public bool eating = false;
    SpriteRenderer sprite;

    float flipTimer;
	// Use this for initialization
	void Awake () {
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - flipTimer > 1f)
        {
            flipTimer = Time.time;
            sprite.flipX = !sprite.flipX;
        }
	}

    public void LoadCreature(Stats savedStats)
    {
        stats = savedStats;
    }

    public void Eat(FruitBehavior fruit)
    {
        StartCoroutine(Eating(fruit));
    }

    IEnumerator Eating(FruitBehavior fruit)
    {
        eating = true;

        yield return new WaitForSeconds(1f);
        fruit.transform.localScale /= 2;
        yield return new WaitForSeconds(1f);
        fruit.transform.localScale /= 2;
        yield return new WaitForSeconds(1f);
        fruit.transform.localScale /= 2;
        yield return new WaitForSeconds(1f);

        stats.Hunger += fruit.HungerAmount;
        stats.Endurance += fruit.EnduranceAmount;

        stats.Hunger = Mathf.Clamp(stats.Hunger, 0, 20);

        GameController.Instance.UpdatePanel(this);
        GameController.Instance.Save();
        Destroy(fruit.gameObject);
        eating = false;
    }
}
