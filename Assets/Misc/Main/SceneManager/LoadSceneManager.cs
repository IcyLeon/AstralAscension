using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] private SceneField[] AwakeSceneNames;

    private void Awake()
    {
        LoadScenes(AwakeSceneNames);
    }

    public void LoadScenes(SceneField[] SceneFieldList)
    {
        foreach (var SceneField in SceneFieldList)
        {
            SceneManager.LoadSceneAsync(SceneField.SceneName, LoadSceneMode.Additive);
        }
    }
}
