using UnityEngine;
using System;

public class EnemyStats : Damageable
{
    [SerializeField] private float expForDeath;
    [SerializeField] private int moneyForDeath;

    public static Action<float, int> OnEnemyDied;

    protected override void Die()
    {
        Debug.Log($"{name} died!");

        OnEnemyDied?.Invoke(expForDeath, moneyForDeath);

        base.Die();
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;

        GameObject createdText = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity, Environment.Instance.effectsParent);
        var floatingTXT = createdText.GetComponent<FloatingText>();
        floatingTXT.SetText(damage.ToString("0"));
    }
}
