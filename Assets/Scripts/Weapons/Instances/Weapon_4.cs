using UnityEngine;

public class Weapon_4 : Weapon
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
        var projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity, Environment.Instance.trashParent);
        var projectile = projectileGO.GetComponent<Projectile>();

        projectile.Initialize(Damage, speed, lifetime, GetRandomDir());

        timeBtwShots = startTimeBtwShots;
    }

    private Vector2 GetRandomDir()
    {
        Vector2 dir = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return dir.normalized;
    }

    public override void Attack()
    {
        timeBtwShots -= Time.fixedDeltaTime;
        if (timeBtwShots <= 0)
            Shoot();
    }
}
