using UnityEngine;

public class Pickup_Money : Pickup
{
    public override void OnPickUp()
    {
        GameCore.Instance.Money += (int)amount;

        base.OnPickUp();
    }
}
