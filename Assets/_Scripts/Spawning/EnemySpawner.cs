using System.Collections;
using UnityEngine;
using System;
using static UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRadius;
    [SerializeField] private float timeBtwWaves;
    [SerializeField] private Wave[] waves;

    private Wave currentWave;
    private float timeBtwSpawns;
    private int enemiesAlive = 0;

    private Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    public static Action OnPlayerWon;

    private void Awake()
    {
        StartCoroutine(StartWaves());
    }

    private void OnEnable()
    {
        EnemyStats.OnEnemyDied += (_, _) => enemiesAlive--;
    }

    private void OnDisable()
    {
        EnemyStats.OnEnemyDied -= (_, _) => enemiesAlive--;
    }

    private IEnumerator StartWaves()
    {
        for (int w = 0; w < waves.Length; w++)
        {
            yield return new WaitForSeconds(timeBtwWaves);

            currentWave = waves[w];
            timeBtwSpawns = 1 / currentWave.spawnEnemiesPerSecond;

            Debug.Log("Starting a new wave...");

            for (int e = 0; e < currentWave.enemiesCount; e++)
            {
                yield return new WaitForSeconds(timeBtwSpawns);

                if (PlayerStats.Instance == null)
                    StopAllCoroutines();

                SpawnEnemy(currentWave.enemyPrefab);
            }
        }

        yield return new WaitUntil(() => enemiesAlive <= 0);

        PlayerWon();
    }

    private void PlayerWon()
    {
        Debug.Log("---===PLAYER WON!!===---");

        OnPlayerWon?.Invoke();
    }

    private void SpawnEnemy(EnemyStats enemyPrefab)
    {
        Instantiate(enemyPrefab, Position + GetRandomPos(), Quaternion.identity, Environment.Instance.enemiesParent);
        enemiesAlive++;
    }

    private Vector2 GetRandomPos()
    {
        // Calculates the random pos on circle surface
        Vector2 pos = new(Range(-1f, 1f), Range(-1f, 1f));
        pos = pos.normalized * spawnRadius;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Position, spawnRadius);
    }
}
