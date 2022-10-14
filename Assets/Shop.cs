using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    private void SetShopPanelActive(bool active) => shopPanel.SetActive(active);

    public void OnCloseButton()
    {
        SetShopPanelActive(false);
    }

    public void OnShopButton()
    {
        SetShopPanelActive(true);
    }
}
