using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFighting : MonoBehaviour
{
    [SerializeField] private float startDamage;
    [SerializeField] private float increaseDamagePerLevel;
    [SerializeField] private float increasePow;
    [SerializeField] private TextMeshProUGUI damageText;

    private List<Weapon> weapons;
    public List<Weapon> Weapons
    {
        get => weapons;
        private set => weapons = value;
    }

    private float damage;

    public float Damage
    {
        get => damage;
        private set
        {
            damage = value;

            damageText.text = Damage.ToString("0");
        }
    }

    private void OnEnable()
    {
        PlayerStats.OnLevelUp += IncreaseDamage;
    }

    private void OnDisable()
    {
        PlayerStats.OnLevelUp -= IncreaseDamage;
    }

    private void Awake()
    {
        var startWeapons = GetComponentsInChildren<Weapon>();

        Weapons = new();
        foreach (var weapon in startWeapons)
            Weapons.Add(weapon);
    }

    private void Start()
    {
        var damageMult = 1 + GameCore.Instance.PlayerProperty.DamageMult;
        Damage = startDamage * damageMult;
    }

    public bool HasWeaponOfType(WeaponType type)
    {
        foreach (var weapon in Weapons)
        {
            if (weapon.WeaponType == type)
                return true;
        }

        return false;
    }

    public void AddWeapon(Weapon weapon)
    {
        Weapons.Add(weapon);
    }

    public void IncreaseDamage(int _)
    {
        var damageMult = 1 + GameCore.Instance.PlayerProperty.DamageMult;
        Damage = Mathf.Pow((Damage + increaseDamagePerLevel) * damageMult, increasePow);

        Debug.Log($"Current damage: {damage}");
    }
}
