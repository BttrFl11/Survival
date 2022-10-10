using System.IO;
using UnityEngine;
using TMPro;

public class GameCore : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Content")]
    [SerializeField] private PlayerProperty playerProperty;

    [Header("Save Config")]
    [SerializeField] private string saveFileName = "data.json";

    private string savePath;

    public int Money
    {
        get => playerProperty.Money;
        set
        {
            playerProperty.Money = value;
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
        PlayerProperty playerProperty = new PlayerProperty
        {
            Money = this.playerProperty.Money
        };

        string data = JsonUtility.ToJson(playerProperty, true);

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
        PlayerProperty playerProperty = JsonUtility.FromJson<PlayerProperty>(data);

        this.playerProperty = playerProperty;

        Money = playerProperty.Money;
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
