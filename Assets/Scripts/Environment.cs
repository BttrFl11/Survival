using UnityEngine;

public class Environment : MonoBehaviour
{
    public Transform trashParent;
    public Transform enemiesParent;
    public Transform effectsParent;

    public static Environment Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Scene has 2 and more Environments!");
    }
}
