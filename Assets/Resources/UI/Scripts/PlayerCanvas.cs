using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanvas : MonoBehaviour
{
    private Player Player;

    private void Awake()
    {
        InitCamera();

        Player = GetComponentInParent<Player>();

        Player.PlayerInteractSensor.OnInteractEnter += OnInteractEnter;
        Player.PlayerInteractSensor.OnInteractExit += OnInteractExit;
    }

    private void InitCamera()
    {
        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }

    private void OnDestroy()
    {
        Player.PlayerInteractSensor.OnInteractEnter -= OnInteractEnter;
        Player.PlayerInteractSensor.OnInteractExit -= OnInteractExit;
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
