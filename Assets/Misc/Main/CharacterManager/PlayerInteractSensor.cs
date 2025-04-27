using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractSensor : InteractSensor
{
    private Dictionary<Transform, IPointOfInterest> interactable_List;
    private Player player;
    private UIController uiController;
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
        CreateUIController();
        uiController.worldInputAction.Interact.started += Interact_started;
    }
    private void UnsubscribeEvent()
    {
        CreateUIController();
        uiController.worldInputAction.Interact.started -= Interact_started;
    }

    private void CreateUIController()
    {
        if (uiController != null)
            return;

        uiController = UIController.instance;
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
