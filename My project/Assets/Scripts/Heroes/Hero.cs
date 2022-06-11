using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public virtual void OnCastHandler(GameObject caster) { }
    public virtual void OnHitHandler(string message) { }

    public virtual void OnEnemySpawn(GameObject spawned) { }
    public virtual bool Cast() { return false; }
}
