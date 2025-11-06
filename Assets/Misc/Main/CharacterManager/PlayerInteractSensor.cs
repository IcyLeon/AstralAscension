using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerInteractSensor : InteractSensor
{
    private Dictionary<Transform, IPointOfInterest> interactable_List;
    private Player player;
    public event OnInteractEvent OnInteractableEnter;
    public event OnInteractEvent OnInteractableExit;

    private UIController uiController;

    protected override void Awake()
    {
        base.Awake();
        uiController = UIController.instance;
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

    private void OnEnable()
    {
        SubscribeEvent();
    }

    private void OnDisable()
    {
        UnsubscribeEvent();
    }

    private void SubscribeEvent()
    {
        uiController.worldInputAction.Interact.started += Interact_started;
    }
    private void UnsubscribeEvent()
    {
        uiController.worldInputAction.Interact.started -= Interact_started;
    }

    private void OnDestroy()
    {
        UnsubscribeEvent();
        OnPOIInteractEnter -= PlayerInteractSensor_OnPOIInteractEnter;
        OnPOIInteractExit -= PlayerInteractSensor_OnPOIInteractExit;
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
