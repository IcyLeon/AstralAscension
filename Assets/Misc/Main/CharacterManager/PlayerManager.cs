using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    public Player player { get; private set; }

    [SerializeField] private GameObject PlayerPrefab;

    [SerializeField] private Player Player;

    private void SpawnPlayer()
    {
        GameObject playerGO = Instantiate(PlayerPrefab);
        player = playerGO.GetComponent<Player>();
    }

    private void Awake()
    {
        instance = this;
        player = Player;
    }

    void Start()
    {
        //SpawnPlayer();
    }
}
