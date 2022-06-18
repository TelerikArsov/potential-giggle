using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController
{
    public delegate void EnemyDeathHandler(GameObject deceased, GameObject projectile);
    public event EnemyDeathHandler OnEnemyDeath;

    public delegate void EnemySpawnHandler(GameObject enemy);
    public event EnemySpawnHandler OnEnemySpawn;

    public GameObject enemy = Resources.Load<GameObject>("Prefabs/Enemies/Goblin");

    private List<GameObject> enemies = new List<GameObject>();

    public EnemiesController()
    {
        
    }

    public float spawnTime = 10.0f;
    public int waveSize = 10;
    public float movementSpeed = 70f;
    public int THRESHOLD = 100;
    private float nextSpawn = 0f;
    private Vector3 target = new Vector3(0, 0, 0);
    public void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;

            Bounds bounds = enemy.GetComponent<Collider2D>().bounds;
            //Debug.Log(bounds); // TODO not working

            int its = 0;
            for (int i = 0; i < waveSize; i++)
            {
                Vector2 spawnPoint;
                while (true)
                {
                    spawnPoint = new Vector2(
                        Random.Range(-5f, 5f),
                        Random.Range(-5f, 5f)
                    );

                    if (!Physics2D.OverlapArea(spawnPoint - new Vector2(1, 1), spawnPoint + new Vector2(1, 1))) // TODO overlap according to collider
                    {
                        break;
                    }

                    its++;
                    if (its > THRESHOLD)
                    {
                        break;
                    }
                }
                if (its > THRESHOLD)
                {
                    break;
                }

                GameObject spawned = Object.Instantiate(
                    enemy,
                    spawnPoint,
                    Quaternion.identity
                );

                enemies.Add(spawned);
                OnEnemySpawn?.Invoke(spawned);
                target = new Vector3(
                    Random.Range(-10f, 10f),
                    Random.Range(-10f, 10f),
                    0
                );

                spawned.GetComponent<CircleCollider2D>().radius *= Random.Range(1.0f, 1.1f);
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (GameObject enemy in enemies)
        {
            Vector3 playerPosition = target;
            Vector3 direction = (playerPosition - enemy.transform.position).normalized;
            enemy.GetComponent<Rigidbody2D>().AddForce(direction * 20f * Random.Range(0, 1f));
        }
    }
}
