using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterNavigationButton : NavigationButton
{
    [SerializeField] private SceneField SceneField;

    protected override void OnClick()
    {
        AsyncOperation AsyncOperation = SceneManager.LoadSceneAsync(SceneField.SceneName, LoadSceneMode.Additive);
        AsyncOperation.completed += AsyncOperation_completed;
    }

    private void LoadStorage()
    {
        CharacterManager characterManager = CharacterManager.instance;

        if (characterManager == null)
            return;

        CharacterScreenMiscEvent.Load(characterManager.characterStorage);
    }

    private void AsyncOperation_completed(AsyncOperation AsyncOperation)
    {
        LoadStorage();
        AsyncOperation.completed -= AsyncOperation_completed;
    }
}
