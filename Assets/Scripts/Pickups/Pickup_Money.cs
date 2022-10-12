using UnityEngine;

public class Pickup_Money : Pickup
{
    private GameCore gameCore;

    private void Awake()
    {
        gameCore = FindObjectOfType<GameCore>();
    }

    public override void OnPickUp()
    {
        gameCore.Money += (int)amount;

        base.OnPickUp();
    }
}
