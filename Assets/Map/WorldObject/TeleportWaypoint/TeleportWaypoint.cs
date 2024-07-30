using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportWaypoint : InteractiveMapObject, IInteractable
{
    public void Interact(Transform sourceTransform)
    {
        CallOnMapObjectChanged();
    }
}
