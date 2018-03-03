using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Text Name;
    public Text Strength;
    public Text Agility;
    public Text Endurance;
    public Text Hunger;
    public Text Happiness;

    public Text moneyCount;

    public GameObject AwardPanel;
    public Text awardName;
    public Text awardGain;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StatsPanel(CreatueBehavior creature)
    {
        anim.SetTrigger("ShowStats");

        Name.text = creature.stats.Name;
        Strength.text = creature.stats.Strength.ToString();
        Agility.text = creature.stats.Agility.ToString();
        Endurance.text = creature.stats.Endurance.ToString();
        Hunger.text = creature.stats.Hunger.ToString();
        Happiness.text = creature.stats.Happiness.ToString();
    }

    public void ShopPanel()
    {
        anim.SetTrigger("ShowShop");
    }

    public void UpdateMoney(int amount)
    {
        moneyCount.text = amount.ToString();
    }

    public void Award(string name, int gain)
    {
        AwardPanel.SetActive(true);

        awardName.text = name + " went for a run!";
        awardGain.text = "They earned: " + gain.ToString();
    }

    public void HidePanels()
    {
        anim.SetTrigger("Hide");

        AwardPanel.SetActive(false);
    }

    public void UpdatePanel(CreatueBehavior creature)
    {
        Name.text = creature.stats.Name;
        Strength.text = creature.stats.Strength.ToString();
        Agility.text = creature.stats.Agility.ToString();
        Endurance.text = creature.stats.Endurance.ToString();
        Hunger.text = creature.stats.Hunger.ToString();
        Happiness.text = creature.stats.Happiness.ToString();
    }
}
