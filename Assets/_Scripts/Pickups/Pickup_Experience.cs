using UnityEngine;

public class Pickup_Experience : Pickup
{
    [SerializeField] private float speed;

    private bool moveToPlayer = false;

    private void FixedUpdate()
    {
        if (moveToPlayer == true)
            transform.position = Vector2.MoveTowards(transform.position, PlayerStats.Instance.transform.position, speed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, PlayerStats.Instance.transform.position) <= 0.1f)
            PickUp();
    }

    private void PickUp()
    {
        PlayerStats.Instance.TakeExp(amount);

        Destroy(gameObject);
    }

    public override void OnPickUp()
    {
        moveToPlayer = true;
    }
}
