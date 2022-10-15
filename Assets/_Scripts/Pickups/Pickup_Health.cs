using UnityEngine;

public class Pickup_Health : Pickup
{
    public override void OnPickUp()
    {
        PlayerStats.Instance.Heal(amount);

        base.OnPickUp();
    }
}
