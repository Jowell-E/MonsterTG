using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumbbell : WorkStation {

    protected override void Work()
    {
        base.Work();

        occupant.stats.Strength += GrowthAmount;
    }

}
