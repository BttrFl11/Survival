using System.IO;
using UnityEngine;
using TMPro;

public class GameCore : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Content")]
    public PlayerProperty PlayerProperty;
    public ShopItemStruct[] ShopItems;

    [Header("Save Config")]
    [SerializeField] private string saveFileName = "data.json";

    private string savePath;

    public int Money
    {
        get => PlayerProperty.Money;
        set
        {
            PlayerProperty.Money = value;

            moneyText.text = Money.ToString();
        }
    }

    public static GameCore Instance;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif

        Initialize();
        LoadFromFile();
    }

    private void OnEnable()
    {
        SceneLoader.OnSceneLoading += SaveToFile;
    }

    private void OnDisable()
    {
        SceneLoader.OnSceneLoading -= SaveToFile;
    }

    private void Initialize()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Scene has 2 and more GameCore!!");
    }

    private void SaveToFile()
    {
        GameCoreStruct gameCoreStruct = new GameCoreStruct
        {
            PlayerProperty = PlayerProperty,
            ShopItems = ShopItems,
        };

        string data = JsonUtility.ToJson(gameCoreStruct, true);

        try
        {
            File.WriteAllText(savePath, data);
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"GameCore error: {ex}");
        }
    }

    private void LoadFromFile()
    {
        if(File.Exists(savePath) == false)
        {
            Debug.Log($"GameCore: file {savePath} dont exists");
            return;
        }

        string data = File.ReadAllText(savePath);
        GameCoreStruct gameCoreStruct = JsonUtility.FromJson<GameCoreStruct>(data);

        PlayerProperty = gameCoreStruct.PlayerProperty;
        ShopItems = gameCoreStruct.ShopItems;
        Money = PlayerProperty.Money;
    }

    private void OnApplicationQuit()
    {
        SaveToFile();
    }

    private void OnApplicationPause(bool pause)
    {
        if (Application.platform == RuntimePlatform.Android)
            SaveToFile();
    }
}
