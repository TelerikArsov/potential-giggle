using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float rotationSpeed;
    private float rotationAngle;
    private float currentRotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(1, 10);
        rotationAngle = Random.Range(0, 10);
        //currentRotation = 
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += rotationSpeed * Time.deltaTime;
        var matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.Euler(0, 0, currentRotation), new Vector3(1, 1, 1));
        GetComponent<SpriteRenderer>().material.SetMatrix("_Matrix", matrix);
    }
}
