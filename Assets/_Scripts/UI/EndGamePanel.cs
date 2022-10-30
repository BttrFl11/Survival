using TMPro;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    [SerializeField] protected float showTimer = 1f;
    [SerializeField] protected GameObject panel;
    [SerializeField] protected TextMeshProUGUI enemiesKilledText;

    private void Awake()
    {
        panel.SetActive(false);
    }

    protected virtual void OnEnable()
    {
        PlayerStats.OnPlayerDied += () => Invoke(nameof(ShowPanel), showTimer);
    }

    protected virtual void OnDisable()
    {
        PlayerStats.OnPlayerDied -= () => Invoke(nameof(ShowPanel), showTimer);
    }

    protected virtual void ShowPanel()
    {
        panel.SetActive(true);

        enemiesKilledText.text = PlayerStats.Instance.EnemiesKilled.ToString();
    }
}
