using UnityEngine;

public class Weapon_1 : Weapon
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float offset;

    private Vector2 lastDir;
    private PlayerMomement playerMomement;

    private Vector2 PositionL
    {
        get => transform.localPosition;
        set => transform.localPosition = value;
    }

    private Vector3 Rotation
    {
        get => transform.rotation.eulerAngles;
        set => transform.rotation = Quaternion.Euler(value);
    }

    protected override void OnEnable()
    {
        playerMomement = GetComponentInParent<PlayerMomement>();
        base.OnEnable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyStats enemyStats))
            GiveDamage(enemyStats);
    }

    public override void Attack()
    {
        if(playerMomement.GetDeviceType() == PlayerMomement.DeviceType.Android)
        {
            if (playerMomement.Direction != Vector2.zero)
                lastDir = playerMomement.Direction;
        }
        else
        {
            if (playerMomement.Direction == Vector2.zero)
                return;

            lastDir = playerMomement.Direction;
        }

        Vector2 distance = PositionL - lastDir;
        float rotZ = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg + offset;
        float smoothZ = Mathf.LerpAngle(Rotation.z, rotZ, rotationSpeed * Time.fixedDeltaTime);
        Rotation = new(0, 0, smoothZ);
    }
}
