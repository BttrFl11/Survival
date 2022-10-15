using UnityEngine;
using System.Threading.Tasks;

public class Cheat : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private int timer = 300;

    [Header("Health")]
    [SerializeField] private KeyCode healthKey;
    [SerializeField] private int healthPerUse = 25;

    [Header("Money")]
    [SerializeField] private KeyCode moneyKey;
    [SerializeField] private int moneyPerUse = 250;

    [Header("Experience")]
    [SerializeField] private KeyCode expKey;
    [SerializeField] private int expPerUse = 15;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKey(healthKey))
            PlayerStats.Instance.Heal(healthPerUse);
        if (Input.GetKey(expKey))
            PlayerStats.Instance.TakeExp(expPerUse);
        if (Input.GetKey(moneyKey))
            GameCore.Instance.Money += moneyPerUse;

        Task.Delay(timer);
    }
}
