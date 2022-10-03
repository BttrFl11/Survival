using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private float spawnEnemiesPerSecond;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Transform spawnedEnemiesParent;

    private float timeBtwSpawns;

    private Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        timeBtwSpawns = 1 / spawnEnemiesPerSecond;

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(timeBtwSpawns);

        if (PlayerStats.Instance == null)
        {
            StopAllCoroutines();
            yield return null;
        }

        GetRandomEnemy(out GameObject enemy, out Vector2 pos);
        SpawnSingleEnemy(enemy, pos);

        StartCoroutine(SpawnEnemy());
    }

    private void SpawnSingleEnemy(GameObject enemy, Vector2 pos)
    {
        Instantiate(enemy, position + pos, Quaternion.identity, spawnedEnemiesParent);
    }

    // Returns the random enemy prefab and random position on circle surface with radius spawnRaduis
    private void GetRandomEnemy(out GameObject enemy, out Vector2 pos)
    {
        enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

        // Calculates the random pos on circle surface
        pos = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        pos = pos.normalized * spawnRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
