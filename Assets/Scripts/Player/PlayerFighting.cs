using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighting : MonoBehaviour
{
    private List<Weapon> weapons = new();

    private void Awake()
    {
        var startWeapons = GetComponentsInChildren<Weapon>();
        foreach (var weapon in startWeapons)
            weapons.Add(weapon);
    }
}
