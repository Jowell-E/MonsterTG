using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playground : WorkStation {

    protected override void Work()
    {
        occupant.stats.Happiness += GrowthAmount;

        GameController.Instance.Save();
        GameController.Instance.UpdatePanel(occupant);
    }

}
