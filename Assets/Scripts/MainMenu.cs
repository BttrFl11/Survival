using UnityEngine;
using System;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButton()
    {
		try
		{
			SceneLoader.Instance.LoadNext();
		}
		catch (Exception ex)
		{
			Debug.LogError($"SceneLoader exception: {ex}");
		}
    }
}
