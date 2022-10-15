using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Animator crossfadeAnim;
    [SerializeField] private float transitionTime;

    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("Scene has 2 and more SceneLoader!");
    }

    private IEnumerator StartLoading(int index)
    {
        crossfadeAnim.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(index);
    }

    public void Load(int index) => StartCoroutine(StartLoading(index));

    public void Load(string name) => StartCoroutine(StartLoading(SceneManager.GetSceneByName(name).buildIndex));

    public void LoadNext() => StartCoroutine(StartLoading(SceneManager.GetActiveScene().buildIndex + 1));

    public void Reload() => StartCoroutine(StartLoading(SceneManager.GetActiveScene().buildIndex));
}
