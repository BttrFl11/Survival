using UnityEngine;

public partial class PlayerMomement : MonoBehaviour
{
    [SerializeField] private DeviceType deviceType;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed;

    private Vector2 direction;

    public Vector2 Direction
    {
        get => direction;
        private set => direction = value;
    }

    private void FixedUpdate()
    {
        if (deviceType == DeviceType.PC)
            Move_Keyboard();
        else
            Move_Joystick();
    }

    private void Move_Keyboard()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        Direction = new(inputX, inputY);
        Direction = Direction.normalized * moveSpeed;

        transform.Translate(Direction * Time.fixedDeltaTime);
    }

    private void Move_Joystick()
    {
        Direction = new(joystick.Horizontal, joystick.Vertical);
        Direction *= moveSpeed;

        transform.Translate(Direction * Time.fixedDeltaTime);
    }

    public DeviceType GetDeviceType() => deviceType;
}
