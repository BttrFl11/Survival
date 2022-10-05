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
    [SerializeField] private float tempExpAnimSpeed;
    [SerializeField] private float startMaxExp;
    [SerializeField] private float increaseExpPerLevel;
    [SerializeField] private float expPow;

    public static PlayerStats Instance;

    private float currentExp = 0;
    private float maxExp;

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

    private void Awake()
    {
        Initialize();
    }

    protected override void OnEnable()
    {
        maxExp = startMaxExp;
        Exp = 0;

        Health = maxHealth;
    }

    protected override void FixedUpdate()
    {
        // Animating tempExpImage. TempExpImage.fillAmount smoothly increases to expImage.fillAmount
        tempExpImage.fillAmount = Mathf.Lerp(tempExpImage.fillAmount, expImage.fillAmount, tempExpAnimSpeed * Time.fixedDeltaTime);

        base.FixedUpdate();
    }

    private void LevelUp()
    {
        currentExp -= maxExp;
        Health = maxHealth;

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

    protected override void Die()
    {
        healthImage.transform.parent.gameObject.SetActive(false);

        Debug.Log("PLAEYR DIED!");

        Destroy(gameObject);
    }


    internal void TakeExp(float exp)
    {
        Exp += exp;

        Debug.Log($"Player took {exp} exp!\nTotal exp: {Exp}");
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
