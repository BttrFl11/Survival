using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_2 : Weapon
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileLifetime;
    [SerializeField] private float anglePerShot;
    [SerializeField] private int shotsCount;

    private float startTimeBtwShots;
    private float timeBtwShots;

    protected override void OnEnable()
    {
        startTimeBtwShots = 1 / fireRate;
        timeBtwShots = startTimeBtwShots;

        base.OnEnable();
    }

    private void Shoot()
    {
        Vector2 dir = GetRandomDir();

        for (int i = 0; i < shotsCount; i++)
        {
            GameObject projectileGO = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Projectile projectile = projectileGO.GetComponent<Projectile>();

            projectile.Initialize(Damage, projectileSpeed, projectileLifetime, dir);
        }

        timeBtwShots = startTimeBtwShots;
    }

    private Vector2 GetRandomDir()
    {
        Vector2 dir = new(Random.Range(-1f,1f), Random.Range(-1f,1f));
        return dir.normalized;
    }

    public override void Attack()
    {
        timeBtwShots -= Time.fixedDeltaTime;

        if (timeBtwShots <= 0)
            Shoot();
    }
}
