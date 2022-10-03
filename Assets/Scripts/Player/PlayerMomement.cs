using UnityEngine;

public partial class PlayerMomement : MonoBehaviour
{
    [SerializeField] private DeviceType deviceType;
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed;

    private Vector2 direction;

    private Vector2 position
    {
        get { return transform.position; }
        set { transform.position = value; }
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
        direction = new(inputX, inputY);
        direction = direction.normalized * moveSpeed;

        transform.Translate(direction * Time.fixedDeltaTime);
    }

    private void Move_Joystick()
    {
        direction = new(joystick.Horizontal, joystick.Vertical);
        direction *= moveSpeed;

        transform.Translate(direction * Time.fixedDeltaTime);
    }
}
