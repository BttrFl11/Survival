using UnityEngine;

public class WonPanel : MonoBehaviour
{
    [SerializeField] private GameObject playerWonPanel;
    [SerializeField] private float showTimer = 1f;

    private void OnEnable()
    {
        EnemySpawner.OnPlayerWon += () => Invoke(nameof(ShowPanel), showTimer);
    }

    private void ShowPanel() => playerWonPanel.SetActive(true);
}
