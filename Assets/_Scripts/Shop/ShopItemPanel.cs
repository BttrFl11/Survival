using UnityEngine;
using TMPro;

public class ShopItemPanel : MonoBehaviour
{
    [SerializeField] private ShopItemSO shopItem;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private int maxLevel;

    private int currentLevel;
    private GameCore gameCore;

    private void Start()
    {
        gameCore = GameCore.Instance;

        currentLevel = gameCore.ShopItems[(int)shopItem.Type].currentLevel;

        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        nameText.text = shopItem.Name;
        levelText.text = $"{currentLevel} / {maxLevel}";
        costText.text = shopItem.Cost.ToString();
    }

    private void Upgrade()
    {
        gameCore.Money -= shopItem.Cost;
        currentLevel++;

        switch (shopItem.Type)
        {
            case ShopItemSO.ShopType.Health:
                gameCore.PlayerProperty.HealthMult += shopItem.Value;
                break;
            case ShopItemSO.ShopType.Damage:
                gameCore.PlayerProperty.DamageMult += shopItem.Value;
                break;
            case ShopItemSO.ShopType.MoveSpeed:
                gameCore.PlayerProperty.MoveSpeedMult += shopItem.Value;
                break;
        }
        gameCore.ShopItems[(int)shopItem.Type].currentLevel = currentLevel;

        UpdateUI();
    }

    public void OnUpgradeButton()
    {
        if(gameCore.Money >= shopItem.Cost && currentLevel < maxLevel)
        {
            Upgrade();
        }
    }
}
