using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;

    // Start is called before the first frame update
    private void Awake()
    {

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        SpawnPlayer();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(PlayerPrefab);
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
    }

}
