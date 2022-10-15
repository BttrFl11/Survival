using UnityEngine;

public class Fireball_W4 : Projectile
{
    [Header("Mini Projectiles")]
    [SerializeField] private float miniProjSpeed;
    [SerializeField] private float miniProjLifetime;
    [SerializeField] private float miniDamageMult;
    [SerializeField] private float maxRotation;
    [SerializeField] private float offset;
    [SerializeField] private int projectileCount;
    [SerializeField] private GameObject projectilePrefab;

    private void CreateProjectiles()
    {
        Vector2 dir = Vector2.up;
        float currentRot = 0;
        float rotationPerProjectile = maxRotation / projectileCount;

        for (int i = 0; i < projectileCount; i++)
        {
            var projectileGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity, Environment.Instance.trashParent);
            var projectile = projectileGO.GetComponent<Projectile>();

            dir = GetDir(currentRot, dir);
            currentRot += rotationPerProjectile + offset;

            projectile.Initialize(damage * miniDamageMult, miniProjSpeed, miniProjLifetime, dir);
        }
    }

    private Vector2 GetDir(float angle, Vector2 dir)
    {
        float x = Mathf.Cos(angle) * dir.magnitude;
        float y = Mathf.Sin(angle) * dir.magnitude;

        return new(x,y);
    }

    protected override void DestroyProjectile()
    {
        CreateProjectiles();

        base.DestroyProjectile();
    }
}
