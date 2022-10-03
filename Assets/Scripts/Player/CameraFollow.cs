using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField][Range(0.1f, 20f)] float speed;

    private Vector3 offset;

    private Vector3 position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    private void Awake()
    {
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        if (target == null)
            return;

        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 newPos = Vector3.Lerp(position, target.position, speed * Time.fixedDeltaTime);
        newPos += offset;
        position = newPos;
    }
}
