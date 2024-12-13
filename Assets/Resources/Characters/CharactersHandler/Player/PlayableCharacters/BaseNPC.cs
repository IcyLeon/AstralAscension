using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : Characters, IInteractable
{
    public Transform GetIPointOfInterestTransform()
    {
        if (!Animator)
            return transform;

        return Animator.GetBoneTransform(HumanBodyBones.Head);
    }

    public void Interact(Player Player)
    {

    }
}
