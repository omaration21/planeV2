using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3.0f; // Intervalo de generación en segundos
    public float spawnRadius = 5.0f; // Radio dentro del cual se generarán enemigos

    private float spawnTimer = 0.0f;

    void Update()
    {
        // Incrementa el temporizador
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0.0f; 
        }
    }

    void SpawnEnemy()
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomRadius = Random.Range(0f, spawnRadius);

        float spawnX = transform.position.x + randomRadius * Mathf.Cos(randomAngle * Mathf.Deg2Rad);
        float spawnY = transform.position.y + randomRadius * Mathf.Sin(randomAngle * Mathf.Deg2Rad);

        Vector2 randomPosition = new Vector2(spawnX, spawnY);

        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }
}
