using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerFighting : MonoBehaviour
{
    [SerializeField] private float startDamage;
    [SerializeField] private float increaseDamagePerLevel;
    [SerializeField] private float increasePow;
    [SerializeField] private TextMeshProUGUI damageText;

    private List<Weapon> weapons = new();

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
        foreach (var weapon in startWeapons)
            weapons.Add(weapon);

        Damage = startDamage;
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void IncreaseDamage()
    {
        Damage = Mathf.Pow(Damage + increaseDamagePerLevel, increasePow);

        Debug.Log($"Current damage: {damage}");
    }
}
