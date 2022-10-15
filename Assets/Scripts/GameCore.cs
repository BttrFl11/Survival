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

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
#else
        savePath = Path.Combine(Application.dataPath, saveFileName);
#endif

        LoadFromFile();
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

    // Testing
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            Money += 50;
    }
    //

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
