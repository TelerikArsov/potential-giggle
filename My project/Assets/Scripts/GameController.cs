using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public delegate void NotifyCast(GameObject caster);
    public List<GameObject> Heroes;
    public event NotifyCast OnCast;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject heroObject in Heroes)
        {
            Hero hero = heroObject.GetComponent<Hero>();
            OnCast += hero.OnCastHandler;
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (GameObject heroObject in Heroes)
        {
            Hero hero = heroObject.GetComponent<Hero>();
            if (hero.Cast())
            {
                OnCast?.Invoke(heroObject);
            }
        }
    }
}
