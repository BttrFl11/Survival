using UnityEngine;

public class EnemyStats : Damageable
{
    [SerializeField] private float expForDeath;

    protected override void Die()
    {
        Debug.Log($"{name} died!");

        PlayerStats.Instance.TakeExp(expForDeath);

        base.Die();
    }
}
