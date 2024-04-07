using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    [field: SerializeField] public AimRig AimRig { get; private set; }

    [HideInInspector]
    public HairpinTeleporter hairpinTeleporter;
    [field: SerializeField] public GameObject TargetOrb { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        TargetOrb.SetActive(false);
    }
    protected override void Start()
    {
        base.Start();
        characterStateMachine = new KeqingStateMachine(this);
    }
}
