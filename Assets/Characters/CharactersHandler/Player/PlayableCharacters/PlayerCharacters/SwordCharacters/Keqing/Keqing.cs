using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    [field: SerializeField] public GameObject HairpinTeleporterPrefab { get; private set; }
    [field: SerializeField] public AimRig AimLookRig { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        base.Start();
        characterStateMachine = new KeqingStateMachine(this);
    }
}
