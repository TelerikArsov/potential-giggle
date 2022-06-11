using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogue : Hero
{
    public float castTime = 4.0f;
    private float nextFire = 0f;
    public override bool Cast()
    {
        if(Time.time > nextFire)
        {
            //Debug.Log("Rogue MAGE Cast");
            nextFire = Time.time + castTime;
            return true;
        }
        return false;
    }
}
