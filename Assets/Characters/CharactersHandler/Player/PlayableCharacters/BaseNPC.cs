using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : Characters, IInteractable
{
    [SerializeField] private NPCInteract NPCInteract;

    public void Interact()
    {

    }

    protected override void Update()
    {
        base.Update();
        UpdateInteractionTransform(NPCInteract.closestInteractionTransform);
    }
}
