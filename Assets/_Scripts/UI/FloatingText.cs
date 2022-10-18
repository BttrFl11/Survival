using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class FloatingText : MonoBehaviour
{
    [SerializeField] private float lifetime;
    [SerializeField] private float speedY;
    [SerializeField] private float speedX;
    [SerializeField] private float widthX;
    [SerializeField] private TextMeshProUGUI meshProText;

    private float birthTime;
    private CanvasGroup canvasGroup;
    private Vector2 startPos;

    private Vector2 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void OnEnable()
    {
        startPos = Position;
        birthTime = Time.time;

        Invoke(nameof(Destroy), lifetime);
    }

    private void Awake() => canvasGroup = GetComponent<CanvasGroup>();

    private void FixedUpdate()
    {
        // X moving
        float lifetime = Time.time - birthTime;
        Vector2 newPos = Position;
        newPos.x = startPos.x + Mathf.Sin(Mathf.PI * 2 * lifetime / speedX) * widthX;

        // Y moving
        newPos.y += speedY * Time.fixedDeltaTime;

        Position = newPos;

        canvasGroup.alpha = Mathf.Lerp(1, 0, lifetime);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetText(string text)
    {
        meshProText.text = text;
    }
}
