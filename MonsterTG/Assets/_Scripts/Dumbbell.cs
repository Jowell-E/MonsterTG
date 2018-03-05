using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbell : WorkStation {

    protected override void Work()
    {
        occupant.stats.Strength += GrowthAmount;
        occupant.stats.Endurance += GrowthAmount/2;

        base.Work();
    }

}
