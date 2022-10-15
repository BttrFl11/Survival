using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Weapon_2 : Weapon
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    [SerializeField] private int shotsCount;
    [SerializeField] private float timeBtwShots;
    [Header("Projectile")]
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;

    //[SerializeField] private float anglePerShot;

    private float startTimeBtwVolleys;
    private float timeBtwVolleys;
    private bool volleyStarted;

    private List<EnemyStats> enemiesInRange = new();

    protected override void OnEnable()
    {
        startTimeBtwVolleys = 1 / fireRate;
        timeBtwVolleys = startTimeBtwVolleys;

        base.OnEnable();
    }

    private IEnumerator StartVolley()
    {
        volleyStarted = true;

        // It finds a nearest enemy
        Vector2 nearestTarget = Vector2.zero;
        foreach (var enemy in enemiesInRange)
        {
            Vector2 distance = enemy.transform.position - transform.position;
            if (nearestTarget == Vector2.zero || distance.magnitude < nearestTarget.magnitude)
                nearestTarget = distance;
        }

        // Shooting volley
        for (int i = 0; i < shotsCount; i++)
        {
            yield return new WaitForSeconds(timeBtwShots);

            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            projectile.Initialize(Damage, projectileSpeed, projectileLifetime, nearestTarget.normalized);
        }

        volleyStarted = false;
        timeBtwVolleys = startTimeBtwVolleys;
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

    public override void Attack()
    {
        timeBtwVolleys -= Time.fixedDeltaTime;
        if (timeBtwVolleys <= 0 && enemiesInRange.Count > 0 && volleyStarted == false)
            StartCoroutine(StartVolley());
    }
}
