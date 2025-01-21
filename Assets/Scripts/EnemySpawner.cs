using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTime;
    [SerializeField] private GameObject enemy;

    private float lastSpawn;

    private void Start()
    {
        lastSpawn = spawnTime;
    }

    private void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        lastSpawn += Time.deltaTime;
        if (lastSpawn >= spawnTime)
        {
            // Pool this in actual game
            Instantiate(enemy, transform);
            lastSpawn = 0;
        }
    }
}
