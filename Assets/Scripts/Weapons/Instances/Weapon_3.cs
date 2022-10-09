using UnityEngine;
using System.Collections;

public class Weapon_3 : Weapon
{
    [SerializeField] private float fireRate;
    [SerializeField] private float screenOffset;
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
            var meteroidGO = Instantiate(meteroidPrefab, GetMeteroidSpawnPos(), Quaternion.identity);
            var meteroid = meteroidGO.GetComponent<Meteroid>();
            meteroid.Initialize(Damage);

            yield return new WaitForSeconds(delayBtwAttacks);
        }

        isAttacking = false;
        timeBtwAttacks = startTimeBtwAttacks;
    }

    private Vector2 GetBoundsSize()
    {
        var boundsSize = new Vector2(CameraScreen.Width * 2 + screenOffset, CameraScreen.Height * 2 + screenOffset);
        return boundsSize;
    }

    private Vector2 GetMeteroidSpawnPos()
    {
        Vector2 screen = new(CameraScreen.Width + screenOffset, CameraScreen.Height + screenOffset);
        Vector2 spawnPos = position + new Vector2(
            Random.Range(-screen.x, screen.x),
            Random.Range(-screen.y, screen.y));

        return spawnPos;
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
        
        Gizmos.DrawWireCube(transform.position, GetBoundsSize());
    }
}
