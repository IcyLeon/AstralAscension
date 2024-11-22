using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject PlayerPrefab;

    private void SpawnPlayer()
    {
        GameObject player = Instantiate(PlayerPrefab);
    }

    void Awake()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
