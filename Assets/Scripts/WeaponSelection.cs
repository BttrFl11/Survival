using UnityEngine;
using System.Collections.Generic;

public class WeaponSelection : MonoBehaviour
{
    [SerializeField] private GameObject weaponSelection;
    [SerializeField] private SelectionItemButton[] itemButtons;
    [SerializeField] private List<WeaponItem> allWeapons;

    public static PlayerFighting PlayerFighting;

    private void Awake()
    {
        PlayerFighting = FindObjectOfType<PlayerFighting>();

        HideSelection();
    }

    private void OnEnable()
    {
        PlayerStats.OnLevelUp += ShowSelection;
    }

    private void OnDisable()
    {
        PlayerStats.OnLevelUp -= ShowSelection;
    }

    private void SetSelectionActive(bool active)
    {
        weaponSelection.SetActive(active);

        float timeScale = active ? 0 : 1;
        Time.timeScale = timeScale;
    }

    // Calls when player got level up
    private void ShowSelection()
    {
        SetSelectionActive(true);

        ActivateAllItemButtons();
        InitializeItemButtons();
    }

    private void ActivateAllItemButtons()
    {
        foreach (var button in itemButtons)
            button.gameObject.SetActive(true);
    }

    private void DisableButton(GameObject button, ref int disabledButtons)
    {
        button.SetActive(false);
        disabledButtons++;
    }

    // Returns the list of weapon that player dont has it
    private List<WeaponItem> GetWeaponList()
    {
        var tempList = new List<WeaponItem>(allWeapons);
        var weaponList = new List<WeaponItem>();
        foreach (var item in tempList)
            if (PlayerFighting.HasWeaponOfType(item.WeaponType) == false)
                weaponList.Add(item);

        return weaponList;
    }

    private void InitializeSingleItemButton(List<WeaponItem> weaponList, SelectionItemButton button, ref int disabledButtons)
    {
        // Gets random weapon from pool
        var randWeapon = weaponList[Random.Range(0, weaponList.Count)];

        // If player already has that weapon, the button will turn off
        bool dublicate = false;
        foreach (var weapon in PlayerFighting.Weapons)
            if (weapon.WeaponType == randWeapon.WeaponType)
                dublicate = true;

        if (dublicate)
            DisableButton(button.gameObject, ref disabledButtons);
        else
            button.SetWeaponItem(randWeapon);

        weaponList.Remove(randWeapon);

        Debug.Log($"Initialized Weapon Item: {randWeapon.name}\nButton: {button.name}");
    }

    private void InitializeItemButtons()
    {
        var weaponList = GetWeaponList();

        // If disabledButton equals the lingth of itemButtons, then the selection will hide
        int disabledButtons = 0;
        foreach (var button in itemButtons)
        {
            // If weapon list if empty, turn off the panel
            if (weaponList.Count == 0)
                DisableButton(button.gameObject, ref disabledButtons);
            else
                InitializeSingleItemButton(weaponList, button, ref disabledButtons);
        }

        if (disabledButtons == itemButtons.Length)
            HideSelection();
    }

    public void HideSelection() => SetSelectionActive(false);
}
