using UnityEngine;

public class CameraScreen : MonoBehaviour
{
    private static float width;
    private static float height;

    private new Camera camera;

    public static float Height
    {
        get => height;
        private set => height = value;
    }

    public static float Width
    {
        get => width;
        private set => width = value;
    }

    private void Awake()
    {
        camera = GetComponent<Camera>();

        Height = camera.orthographicSize;
        Width = Height * camera.aspect;
    }
}
