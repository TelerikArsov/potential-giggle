using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController
{
    public delegate void EnemyDeathHandler(GameObject deceased, GameObject projectile);
    public event EnemyDeathHandler OnEnemyDeath;

    public delegate void EnemySpawnHandler(GameObject enemy);
    public event EnemySpawnHandler OnEnemySpawn;

    private List<GameObject> enemies = new List<GameObject>();

    public EnemiesController()
    {
        
    }

    public float spawnTime = 10.0f;
    public int waveSize = 10;
    public float movementSpeed = 70f;
    private float nextSpawn = 0f;
    private Vector3 target = new Vector3(0, 0, 0);
    public void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;

            for (int i = 0; i < waveSize; i++)
            {
                GameObject spawned = Object.Instantiate(
                Resources.Load<GameObject>("Prefabs/Enemies/Goblin"),
                    new Vector3(
                        Random.Range(-5f, 5f),
                        Random.Range(-5f, 5f),
                        0
                    ),
                    Quaternion.identity
                );
                enemies.Add(spawned);
                OnEnemySpawn?.Invoke(spawned);
                target = new Vector3(
                        Random.Range(-10f, 10f),
                        Random.Range(-10f, 10f),
                        0
                    );
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            Vector3 playerPosition = target;
            Vector3 direction = (playerPosition - enemy.transform.position).normalized;
            enemy.GetComponent<Rigidbody2D>().velocity = movementSpeed * direction * Time.deltaTime;
        }
    }
}
