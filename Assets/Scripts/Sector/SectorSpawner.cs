using UnityEngine;
using System.Collections.Generic;

public class SectorSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sectorPrefab;

    private BoxCollider2D boxCollider;
    private float distanceBtwSectors;
    private bool sectorsSpawned = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        distanceBtwSectors = Mathf.Max(boxCollider.size.x, boxCollider.size.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerStats _) && sectorsSpawned == false)
            SpawnSectors();
    }

    private void SpawnSectors()
    {
        Debug.Log("Spawning sectors...");

        var pos = transform.position;
        List<Vector2> spawnPositions = new()
        {
            new Vector2(pos.x, pos.y + distanceBtwSectors),
            new Vector2(pos.x, pos.y - distanceBtwSectors),
            new Vector2(pos.x + distanceBtwSectors, pos.y),
            new Vector2(pos.x - distanceBtwSectors, pos.y)
        };

        Sector[] sectors = SectorGroup.Sectors.ToArray();

        foreach (var sector in sectors)
            if (Vector2.Distance(transform.position, sector.transform.position) <= distanceBtwSectors)
                spawnPositions.Remove(sector.transform.position);

        foreach (var position in spawnPositions)
            SpawnSingleSector(position);

        sectorsSpawned = true;
    }

    private void SpawnSingleSector(Vector2 pos)
    {
        Instantiate(sectorPrefab, pos, Quaternion.identity, transform.parent);
    }
}
