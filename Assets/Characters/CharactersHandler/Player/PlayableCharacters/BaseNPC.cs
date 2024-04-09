using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : Characters
{
    [SerializeField] private NPCInteract NPCInteract;

    protected override void Update()
    {
        base.Update();
        UpdateInteractionTransform(NPCInteract.closestInteractionTransform);
    }
}
