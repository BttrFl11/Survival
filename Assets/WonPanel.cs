using UnityEngine;

public class WonPanel : EndGamePanel
{
    protected override void OnEnable()
    {
        EnemySpawner.OnPlayerWon += () => Invoke(nameof(ShowPanel), showTimer);
    }

    protected override void OnDisable()
    {
        EnemySpawner.OnPlayerWon -= () => Invoke(nameof(ShowPanel), showTimer);
    }
}
