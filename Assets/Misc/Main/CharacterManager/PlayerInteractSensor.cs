using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractSensor : InteractSensor
{
    [field: SerializeField] public Player player { get; private set; }
 
    protected override void Start()
    {
        base.Start();
        player.PlayerController.playerInputAction.Interact.started += Interact_started;
    }

    private void OnDestroy()
    {
        player.PlayerController.playerInputAction.Interact.started -= Interact_started;
    }


    private void Interact_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Collider collider = GetObject(closestInteractionTransform);
        if (collider == null)
            return;

        if (collider.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(player.transform);
            Debug.Log("Interact " + interactable);
        }
    }
}
