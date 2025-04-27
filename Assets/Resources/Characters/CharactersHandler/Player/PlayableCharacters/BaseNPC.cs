using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : Characters, IInteractable
{
    [field: SerializeField] public CapsuleCollider MainCollider { get; private set; }
    private InteractSensor interactSensor;

    protected override void Awake()
    {
        base.Awake();
        CreateInteraction();
    }

    private void CreateInteraction()
    {
        interactSensor = gameObject.AddComponent<InteractSensor>();
        interactSensor.CreateCollider(MainCollider.height, MainCollider.center);
    }
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
