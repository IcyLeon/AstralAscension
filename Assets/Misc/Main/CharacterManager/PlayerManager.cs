using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    public Player player { get; private set; }

    [SerializeField] private GameObject PlayerPrefab;

    [field: SerializeField] public Player Player { get; private set; }
    public static event Action<Player> OnPlayerChanged;


    private void SpawnPlayer()
    {
        GameObject playerGO = Instantiate(PlayerPrefab);
        player = playerGO.GetComponent<Player>();
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //SpawnPlayer();
        player = Player;
        OnPlayerChanged?.Invoke(player);
    }
}
