using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractSensor : InteractSensor
{
    private Player player;
    private PlayerController playerController;

    protected override void Start()
    {
        base.Start();
        playerController = PlayerController.instance;
        player = GetComponentInParent<Player>();
        playerController.playerInputAction.Interact.started += Interact_started;
    }

    private void OnDestroy()
    {
        playerController.playerInputAction.Interact.started -= Interact_started;
    }


    private void Interact_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Collider collider = GetObject(closestInteractionTransform);
        if (collider == null)
            return;

        if (collider.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(player);
            Debug.Log("Interact " + interactable);
        }
    }
}
