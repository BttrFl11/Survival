using UnityEngine;

public class Meteroid_W3 : SecondaryWeapon
{
    [SerializeField] private Transform exploidRangeCanvas;
    [SerializeField] private float exploidRadius;

    private void OnEnable()
    {
        exploidRangeCanvas.localScale = new(exploidRadius, exploidRadius);
    }

    public void Exploid()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, exploidRadius);
        foreach (var coll in colls)
        {
            if (coll.TryGetComponent(out EnemyStats enemyStats))
                GiveDamage(enemyStats);
        }

        DestroyProjectile();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, exploidRadius);
    }
}
