using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class Weapon_2 : Shootable
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

    protected override void OnEnable()
    {
        startTimeBtwVolleys = 1 / fireRate;
        timeBtwVolleys = startTimeBtwVolleys;

        base.OnEnable();
    }

    private IEnumerator StartVolley()
    {
        volleyStarted = true;

        // Shooting volley
        for (int i = 0; i < shotsCount; i++)
        {
            yield return new WaitForSeconds(timeBtwShots);

            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            Vector2 nearestTarget = GetNearestEnemyDir();

            projectile.Initialize(Damage, projectileSpeed, projectileLifetime, new Vector2(nearestTarget.x, nearestTarget.y).normalized);
        }

        volleyStarted = false;
        timeBtwVolleys = startTimeBtwVolleys;
    }

    public override void Attack()
    {
        timeBtwVolleys -= Time.fixedDeltaTime;
        if (timeBtwVolleys <= 0 && enemiesInRange.Count > 0 && volleyStarted == false)
            StartCoroutine(StartVolley());
    }
}
