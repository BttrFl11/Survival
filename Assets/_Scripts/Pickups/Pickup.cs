using UnityEngine;

public class Pickup : MonoBehaviour, IPickable
{
    [SerializeField] protected float amount;

    public virtual void OnPickUp()
    {
        Debug.Log("Player has picked up pickup");

        Destroy(gameObject);
    }
}
