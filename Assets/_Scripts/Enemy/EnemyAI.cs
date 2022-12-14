using UnityEngine;

[RequireComponent(typeof(Neighborhood))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float avoidVelocity;
    [SerializeField] private float avoidSpeed;

    private Neighborhood neighborhood;
    private new Rigidbody2D rigidbody;
    private Transform target;

    private Vector2 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Awake()
    {
        neighborhood = GetComponent<Neighborhood>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (PlayerStats.Instance != null)
            target = PlayerStats.Instance.transform;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        AvoidCollision();

        MoveTawardsTarget();
    }

    // Calculates the avoid velocity
    private void AvoidCollision()
    {
        Vector2 velocity = rigidbody.velocity;
        Vector2 avoidVel = Vector2.zero;
        Vector2 avgPos = neighborhood.AVGPosition;

        if (avgPos != Vector2.zero)
        {
            avoidVel = Position - avgPos;
            avoidVel = avoidVel.normalized * avoidVelocity;
        }

        velocity = Vector2.Lerp(velocity, avoidVel, avoidSpeed * Time.fixedDeltaTime);
        rigidbody.velocity = velocity;
    }

    private void MoveTawardsTarget()
    {
        Position = Vector2.MoveTowards(Position, target.position, moveSpeed * Time.fixedDeltaTime);
    }
}
