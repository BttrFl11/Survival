using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectionItemButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;

    private Button button;
    private WeaponItem weaponItem;
    private static PlayerFighting playerFighting;

    public WeaponItem WeaponItem
    {
        get => weaponItem;
        private set
        {
            weaponItem = value;

            if(value != null)
            {
                nameText.text = WeaponItem.name;
                button.interactable = true;
                icon.enabled = true;
                icon.sprite = WeaponItem.Icon;
            }
            else
            {
                nameText.text = "";
                icon.enabled = false;
                button.interactable = false;
            }
        }
    }

    private void Awake()
    {
        button = GetComponent<Button>();

        if (playerFighting == null)
            playerFighting = FindObjectOfType<PlayerFighting>();
    }

    private void CreateWeapon()
    {
        var createdWeaponGO = Instantiate(weaponItem.Prefab.gameObject, playerFighting.transform);
        var createdWeapon = createdWeaponGO.GetComponent<Weapon>();
        playerFighting.AddWeapon(createdWeapon);

        WeaponItem = null;
    }

    public void SetWeaponItem(WeaponItem item)
    {
        WeaponItem = item;

        foreach (var weapon in playerFighting.Weapons)
        {
            if (weapon.WeaponType == WeaponItem.WeaponType)
            {
                WeaponItem = null;
                return;
            }
        }
    }

    public void OnButton()
    {
        Debug.Log($"Creating weapon {WeaponItem.name}");
        CreateWeapon();
    }
}
