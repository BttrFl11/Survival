using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius;
    [SerializeField] private float timeBtwWaves;
    [SerializeField] private Transform spawnedEnemiesParent;
    [SerializeField] private Wave[] waves;

    private Wave currentWave;
    private float timeBtwSpawns;

    private Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        StartCoroutine(StartWaves());
    }

    private IEnumerator StartWaves()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            currentWave = waves[w];
            timeBtwSpawns = 1 / currentWave.spawnEnemiesPerSecond;

            Debug.Log("Starting the new wave...");

            for (int e = 0; e < currentWave.enemiesCount; e++)
            {
                SpawnEnemy(currentWave.enemyPrefab);

                Debug.Log($"An enemy ?{e} were spawned");

                yield return new WaitForSeconds(timeBtwSpawns);
            }

            Debug.Log("Wave ended!");

            yield return new WaitForSeconds(timeBtwWaves);
        }

        Debug.Log("PLAYER WON!");

        yield return null;
    }

    private void SpawnEnemy(EnemyStats enemyPrefab)
    {
        if (PlayerStats.Instance == null)
            StopAllCoroutines();

        Instantiate(enemyPrefab, position + GetRandomPos(), Quaternion.identity, spawnedEnemiesParent);
    }

    private Vector2 GetRandomPos()
    {
        // Calculates the random pos on circle surface
        Vector2 pos = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        pos = pos.normalized * spawnRadius;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}