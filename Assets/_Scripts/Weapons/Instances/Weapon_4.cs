using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Weapon_4 : Shootable
{
    [SerializeField] private float fireRate;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime;
    [SerializeField] private GameObject projectilePrefab;

    private float timeBtwShots;
    private float startTimeBtwShots;

    private void Awake()
    {
        startTimeBtwShots = 1 / fireRate;
        timeBtwShots = startTimeBtwShots;
    }

    private void Shoot()
    {
        Debug.Log("Shot");

        var projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity, Environment.Instance.trashParent);
        var projectile = projectileGO.GetComponent<Projectile>();

        projectile.Initialize(Damage, speed, lifetime, GetNearestEnemyDir());

        timeBtwShots = startTimeBtwShots;
    }

    public override void Attack()
    {
        timeBtwShots -= Time.fixedDeltaTime;
        if (timeBtwShots <= 0 && enemiesInRange.Count > 0)
            Shoot();
    }
}
