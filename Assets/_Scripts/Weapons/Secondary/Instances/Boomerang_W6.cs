using UnityEngine;

public class Boomerang_W6 : Projectile
{
    //[SerializeField] private float range;
    //[SerializeField] private float rotationSpeed;

    //private bool moveBackward = false;
    //private const int BACKWARD_RANGE_MULT = 3;

    //private Vector2 targetPos;

    //private void Start()
    //{
    //    targetPos = Position + direction * range;
    //}

    //protected override void Move()
    //{
    //    if(moveBackward == false)
    //    {
    //        var newPos = Vector2.Lerp(Position, targetPos, speed * Time.fixedDeltaTime);
    //        transform.position = newPos;

    //        if (Vector2.Distance(transform.position, direction * range) <= 0.1f)
    //        {
    //            targetPos = Position - -direction * range;
    //            moveBackward = true;
    //        }
    //    }
    //    else
    //    {
    //        var newPos = Vector2.Lerp(Position, targetPos * BACKWARD_RANGE_MULT, speed * Time.fixedDeltaTime / BACKWARD_RANGE_MULT);
    //        transform.position = newPos;
    //    }
    //}

    //protected override void FixedUpdate()
    //{
    //    transform.Rotate(Vector3.forward * rotationSpeed);

    //    base.FixedUpdate();
    //}

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    base.OnTriggerEnter2D(collision);
    //}
}
