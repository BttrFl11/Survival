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

    public override void TakeDamage(float damage)
    {
        Health -= damage;

        GameObject createdText = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity, Environment.Instance.effectsParent);
        var floatingTXT = createdText.GetComponent<FloatingText>();
        floatingTXT.SetText(damage.ToString("0"));
    }
}
