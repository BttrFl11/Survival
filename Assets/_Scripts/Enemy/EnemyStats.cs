using UnityEngine;
using System;

public class EnemyStats : Damageable
{
    [Header("General")]
    [SerializeField] private float expForDeath;
    [SerializeField] private int moneyForDeath;

    [Header("Blood")]
    [SerializeField] private GameObject blood;
    [SerializeField] private float spawnPosOffset;
    [SerializeField] private float bloodDestroyTimer;

    public static Action<float, int> OnEnemyDied;

    protected override void Die()
    {
        Debug.Log($"{name} died!");

        OnEnemyDied?.Invoke(expForDeath, moneyForDeath);

        CreateBlood(centered: true);

        base.Die();
    }

    private void CreateDamageText(float damage)
    {
        GameObject createdText = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity, Environment.Instance.effectsParent);
        var floatingTXT = createdText.GetComponent<FloatingText>();
        floatingTXT.SetText(damage.ToString("0"));
    }

    private void CreateBlood(bool minimized = false, bool centered = false)
    {
        float randRotZ = UnityEngine.Random.Range(0, 360);
        Quaternion randRot = Quaternion.Euler(0, 0, randRotZ);

        Vector2 spawnPos;
        if (centered)
            spawnPos = transform.position;
        else
            spawnPos = transform.position + new Vector3(UnityEngine.Random.Range(-spawnPosOffset, spawnPosOffset),
                                                          UnityEngine.Random.Range(-spawnPosOffset, spawnPosOffset));
        var bloodGO = Instantiate(blood, spawnPos, randRot, Environment.Instance.trashParent);
        Destroy(bloodGO, bloodDestroyTimer);

        if (minimized)
        {
            var bloodScale = bloodGO.transform.localScale;
            bloodGO.transform.localScale = new(bloodScale.x / 2, bloodScale.y / 2, bloodScale.z);
        }
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;

        CreateDamageText(damage);
        CreateBlood(minimized: true, centered: false); ;
    }
}
