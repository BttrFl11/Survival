using System.Collections;
using UnityEngine;

public class EnemyFighting : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attacksPerSecond;
    [SerializeField] private LayerMask playerLayer;

    private float timeBtwAttacks;
    private new CircleCollider2D collider;

    private void Awake()
    {
        collider = GetComponent<CircleCollider2D>();

        timeBtwAttacks = 1 / attacksPerSecond;
    }

    private IEnumerator StartAttacking(PlayerStats playerStats)
    {
        if (collider.IsTouchingLayers(playerLayer))
        {
            Attack(playerStats);
        }

        yield return new WaitForSeconds(timeBtwAttacks);

        StartCoroutine(StartAttacking(playerStats));
    }

    private void Attack(PlayerStats playerStats)
    {
        playerStats.TakeDamage(damage);

        Debug.Log("Enemy attacks!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerStats playerStats)) 
        {
            StartCoroutine(StartAttacking(playerStats));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats playerStats))
        {
            StopCoroutine(StartAttacking(playerStats));
        }
    }
}
