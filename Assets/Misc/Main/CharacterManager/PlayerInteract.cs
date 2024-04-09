using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : Interact<IInteractable>
{
    [field: SerializeField] public Player player { get; private set; }

    protected override void Start()
    {
        base.Start();
        player.playerInputAction.Interact.started += Interact_started;
    }

    private void OnDestroy()
    {
        player.playerInputAction.Interact.started -= Interact_started;
    }

    private void Interact_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (isInteractableObject())
        {
            Debug.Log("Interact!");
        }

        Debug.Log(GetObject(closestInteractionTransform));
    }
}
