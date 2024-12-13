using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractSensor : InteractSensor
{
    private Dictionary<Transform, IPointOfInterest> interactable_List;

    private Player player;
    private PlayerController playerController;
    public event OnInteractEvent OnInteractableEnter;
    public event OnInteractEvent OnInteractableExit;

    protected override void Awake()
    {
        base.Awake();
        interactable_List = new();
        player = GetComponentInParent<Player>();
        OnPOIInteractEnter += PlayerInteractSensor_OnPOIInteractEnter;
        OnPOIInteractExit += PlayerInteractSensor_OnPOIInteractExit;
    }

    private void PlayerInteractSensor_OnPOIInteractEnter(Collider Collider)
    {
    }

    private void PlayerInteractSensor_OnPOIInteractExit(Collider Collider)
    {
    }


    protected override void Start()
    {
        base.Start();
        playerController = PlayerController.instance;
        playerController.playerInputAction.Interact.started += Interact_started;
    }

    private void OnDestroy()
    {
        OnPOIInteractEnter -= PlayerInteractSensor_OnPOIInteractEnter;
        OnPOIInteractExit -= PlayerInteractSensor_OnPOIInteractExit;
        playerController.playerInputAction.Interact.started -= Interact_started;
    }


    private void Interact_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IInteractable interactable = currentClosestPOI as IInteractable;

        if (interactable == null)
            return;

        interactable.Interact(player);
        Debug.Log("Interact " + interactable);
    }
}
