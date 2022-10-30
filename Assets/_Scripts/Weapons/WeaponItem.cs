using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "Weapon")]
public class WeaponItem : ScriptableObject
{
    public Sprite Icon;
    public Weapon Prefab;
    public WeaponType WeaponType;
}

public enum WeaponType
{
    Axe,
    Sword,
    Magic_staff,
    Meteroid,
    Fireball,
    Flamethromwer,
    Boomerang,
}
