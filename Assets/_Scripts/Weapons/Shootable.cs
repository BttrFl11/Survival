using System.Collections.Generic;
using UnityEngine;

public class Shootable : Weapon
{
    protected List<EnemyStats> enemiesInRange = new();

    public override void Attack()
    {
        
    }

    protected virtual Vector2 GetNearestEnemyDir()
    {
        Vector2 nearestTarget = Vector2.zero;
        foreach (var enemy in enemiesInRange)
        {
            Vector2 distance = enemy.transform.position - transform.position;
            if (nearestTarget == Vector2.zero || distance.magnitude < nearestTarget.magnitude)
                nearestTarget = distance;
        }

        return nearestTarget;
    }

    // If an enemy has entered in the fire range, adds it to the list
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            enemiesInRange.Add(enemyStats);
    }

    // If an enemy has died or left from the fire range, removes it from the list
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            enemiesInRange.Remove(enemyStats);
    }
}
