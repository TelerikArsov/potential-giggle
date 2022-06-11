using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMage : Hero
{
    public float castTime = 10.0f;
    private float nextFire = 0f;
    public override bool Cast()
    {
        if(Time.time > nextFire)
        {
            //Debug.Log("FIRE MAGE Cast");
            nextFire = Time.time + castTime;
            return true;
        }
        return false;
    }

    public override void OnCastHandler(GameObject caster)
    {
        if(caster != gameObject)
        {
            //Debug.Log("ON CAST. FIRE MAGE HANDLER");
        }
    }

    public override void OnEnemySpawn(GameObject spawned)
    {
        Debug.Log("ENEMY SPAWNED ZAPZ ZAP");
    }
}
