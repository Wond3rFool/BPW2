using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : PowerupEffect
{
    public float amount;

    public override void Apply(GameObject target)
    {
        target.GetComponent<Health>().health += amount;
    }


}
