using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;

    private void Awake()
    {
        gameOverPanel.SetActive(false);
    }

    private void OnEnable()
    {
        PlayerStats.OnPlayerDied += ShowGameOverPanel;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerDied -= ShowGameOverPanel;
    }

    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);

        enemiesKilledText.text = PlayerStats.Instance.EnemiesKilled.ToString();
    }
}
