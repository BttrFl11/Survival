using UnityEngine;
using System;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private int menuSceneIndex;

    private void SetSettingsPanelActive(bool active) => settingsPanel.SetActive(active);

    public void OnSettingsButton()
    {
        SetSettingsPanelActive(true);
    }

    public void OnResumeButton()
    {
        SetSettingsPanelActive(false);
    }

    public void OnMenuButton()
    {
        try
        {
            SceneLoader.Instance.Load(menuSceneIndex);
        }
        catch (Exception ex)
        {
            Debug.LogError($"SceneLoader exception: {ex}");
        }

    }
}
