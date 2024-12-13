using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    private Player Player;

    private void Awake()
    {

        Player = GetComponentInParent<Player>();

        Player.PlayerInteractSensor.OnInteractableEnter += OnInteractableEnter;
        Player.PlayerInteractSensor.OnInteractableExit += OnInteractableExit;
    }

    private void Start()
    {
        InitCamera();
    }

    private void InitCamera()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
            canvas.planeDistance = 1f;
        }
    }

    private void OnDestroy()
    {
        Player.PlayerInteractSensor.OnInteractableEnter -= OnInteractableEnter;
        Player.PlayerInteractSensor.OnInteractableExit -= OnInteractableExit;
    }

    private void OnInteractableEnter(Collider collider)
    {
    }
    private void OnInteractableExit(Collider collider)
    {
    }


    // Update is called once per frame
    private void Update()
    {
        
    }
}
