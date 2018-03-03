using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : WorkStation {

    protected override void Work()
    {
        base.Work();

        occupant.stats.Agility += GrowthAmount;
    }

}
