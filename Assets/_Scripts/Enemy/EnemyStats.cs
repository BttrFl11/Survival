using UnityEngine;
using System;
using static UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyStats : Damageable
{
    [Header("General")]
    [SerializeField] private float expForDeath;
    [SerializeField] private int moneyForDeath;
    [SerializeField] private GameObject dropForDeath; 
    [SerializeField][Range(0,1)] private float pushResist;

    [Header("Blood")]
    [SerializeField] private GameObject[] bloodPrefabs;
    [SerializeField] private float spawnPosOffset;
    [SerializeField] private float bloodDestroyTimer;

    private new Rigidbody2D rigidbody;

    public static Action<float, int> OnEnemyDied;

    protected override float Health
    {
        get { return health; }
        set
        {
            health = value;

            if (health > maxHealth)
                health = maxHealth;
            else if (health <= 0)
                Die();
        }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Die()
    {
        Debug.Log($"{name} died!");

        OnEnemyDied?.Invoke(expForDeath, moneyForDeath);

        CreateBlood(centered: true);
        SpawnDrop();

        base.Die();
    }

    private void SpawnDrop()
    {
        Instantiate(dropForDeath, transform.position, Quaternion.identity, Environment.Instance.trashParent);
    }

    private void CreateDamageText(float damage)
    {
        GameObject createdText = Instantiate(floatingText.gameObject, transform.position, Quaternion.identity, Environment.Instance.effectsParent);
        var floatingTXT = createdText.GetComponent<FloatingText>();
        floatingTXT.SetText(damage.ToString("0"));
    }

    private void CreateBlood(bool minimized = false, bool centered = false)
    {
        float randRotZ = Range(0, 360);
        Quaternion randRot = Quaternion.Euler(0, 0, randRotZ);

        Vector2 spawnPos;
        if (centered)
            spawnPos = transform.position;
        else
            spawnPos = transform.position + new Vector3(Range(-spawnPosOffset, spawnPosOffset),
                                                        Range(-spawnPosOffset, spawnPosOffset));

        var randBloodPrefab = bloodPrefabs[Range(0, bloodPrefabs.Length)];
        var bloodGO = Instantiate(randBloodPrefab, spawnPos, randRot, Environment.Instance.trashParent);
        Destroy(bloodGO, bloodDestroyTimer);

        if (minimized)
        {
            var bloodScale = bloodGO.transform.localScale;
            bloodGO.transform.localScale = new(bloodScale.x / 2, bloodScale.y / 2, bloodScale.z);
        }
    }

    private void PushFromPlayer(float pushForce)
    {
        var dir = transform.position - PlayerStats.Instance.transform.position;
        float force = pushForce / pushResist;

        rigidbody.AddForce(dir.normalized * force, ForceMode2D.Impulse);
    }

    public override void TakeDamage(float damage)
    {
        CreateBlood(minimized: true, centered: false);
        CreateDamageText(damage);

        Health -= damage;
    }

    public override void TakeDamage(float damage, float pushForce)
    {
        PushFromPlayer(pushForce);

        CreateDamageEffect();
        CreateDamageText(damage);
        CreateBlood(minimized: true, centered: false);

        Health -= damage;
    }
}
