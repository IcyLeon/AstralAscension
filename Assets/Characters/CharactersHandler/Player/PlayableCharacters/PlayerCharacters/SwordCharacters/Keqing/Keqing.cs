using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    [field: SerializeField] public GameObject HairpinTeleporterPrefab { get; private set; }
    [field: SerializeField] public AimRigController AimRigController { get; private set; }

    [SerializeField] ElementsSO test;
    public KeqingReuseableData keqingReuseableData
    {
        get
        {
            return characterReuseableData as KeqingReuseableData;
        }
    }

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(this, GetElementsSO(), 1000f);
            TakeDamage(this, test, 1000f);
        }
    }
    protected override void Start()
    {
        base.Start();
        characterStateMachine = new KeqingStateMachine(this);
    }
}
