using UnityEngine;

[CreateAssetMenu(fileName = "new item", menuName = "Shop Item")]
public class ShopItemSO : ScriptableObject
{
    public ShopType Type;
    public string Name;
    public int Cost;
    public float Value;

    public enum ShopType
    {
        Health,
        Damage,
        MoveSpeed,
    }
}
