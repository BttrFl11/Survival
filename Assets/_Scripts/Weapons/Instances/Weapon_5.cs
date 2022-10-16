using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Weapon_5 : Weapon_1
{
    [SerializeField] private float attackTime;
    [SerializeField] private float reloadTime;
    [SerializeField] private float timeBtwAttacks;
    [SerializeField] private ParticleSystem flameEffect;

    private float timer;
    protected bool reloading = true;

    private List<EnemyStats> enemiesInRange = new();

    protected override void OnEnable()
    {
        timer = reloadTime;
        flameEffect.Stop();

        base.OnEnable();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            enemiesInRange.Add(enemyStats);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            enemiesInRange.Remove(enemyStats);
    }

    protected override void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;
        if (timer <= 0)
        {
            timer = reloading ? attackTime : reloadTime;

            if (reloading == true)
            {
                StartCoroutine(AttackEnemies());
                flameEffect.Play();
            }
            else
            {
                StopAllCoroutines();
                flameEffect.Stop();
            }

            reloading = !reloading;
        }

        base.FixedUpdate();
    }

    private IEnumerator AttackEnemies()
    {
        foreach (var enemy in enemiesInRange.ToList())
        {
            if (enemy != null)
                GiveDamage(enemy);
        }

        yield return new WaitForSeconds(timeBtwAttacks);

        StartCoroutine(AttackEnemies());
    }
}
