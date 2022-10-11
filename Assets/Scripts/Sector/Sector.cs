using UnityEngine;

public class Sector : MonoBehaviour
{
    [SerializeField] private SectorContent[] sectorContent;

    private BoxCollider2D boxCollider;
    private Vector2 boxColliderSize;

    private Vector2 position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        boxColliderSize = boxCollider.size;
    }

    private void OnEnable()
    {
        SectorGroup.Sectors.Add(this);

        Invoke(nameof(SpawnContents), 0.1f);
    }

    private void OnDisable()
    {
        SectorGroup.Sectors.Remove(this);
    }

    private void SpawnContents()
    {
        foreach (var content in sectorContent)
            for (int i = 0; i < content.Count; i++)
                SpawnSingleContent(content.Prefab);
    }

    private void SpawnSingleContent(GameObject prefab)
    {
        Instantiate(prefab, GetRandomSpawnPos(), Quaternion.identity, Environment.Instance.trashParent);
    }

    private Vector2 GetRandomSpawnPos()
    {
        float x = boxColliderSize.x;
        float y = boxColliderSize.y;
        Vector2 pos = position + new Vector2(Random.Range(-x, x) / 2, Random.Range(-y, y) / 2);

        return pos;
    }
}

