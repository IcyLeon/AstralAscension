using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private Player Player;

    private void Awake()
    {
        Player.PlayerInteract.OnInteractEnter += OnInteractEnter;
        Player.PlayerInteract.OnInteractExit += OnInteractExit;
    }

    private void OnDestroy()
    {
        Player.PlayerInteract.OnInteractEnter -= OnInteractEnter;
        Player.PlayerInteract.OnInteractExit -= OnInteractExit;
    }

    private void OnInteractEnter(Collider collider)
    {
    }
    private void OnInteractExit(Collider collider)
    {
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
