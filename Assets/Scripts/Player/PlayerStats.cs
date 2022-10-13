using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerStats : Damageable
{
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("Exprience")]
    [SerializeField] private Image expImage;
    [SerializeField] private Image tempExpImage;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float tempExpAnimSpeed;
    [SerializeField] private float startMaxExp;
    [SerializeField] private float increaseExpPerLevel;
    [SerializeField] private float expPow;

    public static PlayerStats Instance;

    private float currentExp = 0;
    private float maxExp;
    private int curentLevel = 1;
    private GameCore gameCore;

    public static Action OnLevelUp;

    protected override float Health 
    {
        get { return base.Health; }
        set
        {
            base.Health = value;
            healthText.text = Health.ToString("0");
        }
    }

    private float Exp
    {
        get { return currentExp; }
        set
        {
            currentExp = value;

            if (currentExp >= maxExp)
                LevelUp();

            expImage.fillAmount = currentExp / maxExp;
            expText.text = $"{currentExp:0} / {maxExp:0}";
        }
    }

    private int Level
    {
        get => curentLevel;
        set
        {
            curentLevel = value;

            levelText.text = Level.ToString();
        }
    }

    private void Awake()
    {
        gameCore = FindObjectOfType<GameCore>();

        Initialize();

        EnemyStats.OnEnemyDied += OnEnemyDied;
    }

    protected override void OnEnable()
    {
        maxExp = startMaxExp;
        Exp = 0;

        Health = maxHealth;
    }

    // Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            Health += 10;
        if (Input.GetKey(KeyCode.T))
            Exp += 15;
    }
    //

    protected override void FixedUpdate()
    {
        // Animating tempExpImage. TempExpImage.fillAmount smoothly increases to expImage.fillAmount
        tempExpImage.fillAmount = Mathf.Lerp(tempExpImage.fillAmount, expImage.fillAmount, tempExpAnimSpeed * Time.fixedDeltaTime);

        base.FixedUpdate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Pickup pickable))
            pickable.OnPickUp();
    }

    private void LevelUp()
    {
        currentExp -= maxExp;
        Level++;

        maxExp = Mathf.Pow(maxExp + increaseExpPerLevel, expPow);

        OnLevelUp?.Invoke();

        Debug.Log("LEVEL UP!");
    }

    private void Initialize()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Scene has 2 and more PlayerStats!!!");
    }

    private void OnEnemyDied(float exp, int money)
    {
        TakeExp(exp);

        gameCore.Money += money;
    }

    protected override void Die()
    {
        Debug.Log("PLAEYR DIED!");

        healthImage.transform.parent.gameObject.SetActive(false);

        Destroy(gameObject);
    }

    internal void TakeExp(float exp)
    {
        Debug.Log($"Player took {exp} exp!\nTotal exp: {Exp}");

        Exp += exp;
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Heal(float health) => Health += health;
}
