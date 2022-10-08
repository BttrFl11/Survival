using UnityEngine;
using System.Collections;

public class Weapon_3 : Weapon
{
    [SerializeField] private float fireRate;
    [SerializeField] private float attackRange;
    [SerializeField] private float delayBtwAttacks;
    [SerializeField] private int meteroidCount;
    [SerializeField] private GameObject meteroidPrefab;

    private float startTimeBtwAttacks;
    private float timeBtwAttacks;
    private bool isAttacking;

    private Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    protected override void OnEnable()
    {
        startTimeBtwAttacks = 1 / fireRate;
        timeBtwAttacks = startTimeBtwAttacks;

        base.OnEnable();
    }

    private IEnumerator SpawnMeteroids()
    {
        isAttacking = true;

        for (int i = 0; i < meteroidCount; i++)
        {
            Vector2 spawnPos = position + Random.insideUnitCircle * attackRange;
            var meteroidGO = Instantiate(meteroidPrefab, spawnPos, Quaternion.identity);
            var meteroid = meteroidGO.GetComponent<Meteroid>();
            meteroid.Initialize(Damage);

            yield return new WaitForSeconds(delayBtwAttacks);
        }

        isAttacking = false;
        timeBtwAttacks = startTimeBtwAttacks;
    }

    public override void Attack()
    {
        timeBtwAttacks -= Time.fixedDeltaTime;
        if (timeBtwAttacks <= 0 && isAttacking == false)
            StartCoroutine(SpawnMeteroids());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
