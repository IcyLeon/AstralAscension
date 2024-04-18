using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    [SerializeField] private GameObject HairpinTeleporterPrefab;
    private ObjectPool<HairpinTeleporter> objectPool;
    [field: SerializeField] public AimRig AimRig { get; private set; }

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
        objectPool = new ObjectPool<HairpinTeleporter>(HairpinTeleporterPrefab, transform);
        objectPool.ObjectCreated += OnHairPinObjectCreated;
    }

    public HairpinTeleporter activehairpinTeleporter
    {
        get
        {
            return objectPool.GetActivePooledObject();
        }
    }

    public HairpinTeleporter hairpinTeleporter
    {
        get
        {
            return objectPool.GetPooledObject();
        }
    }

    private void OnHairPinObjectCreated(HairpinTeleporter HT)
    {
        if (HT == null)
            return;

        HT.SetPlayableCharacter(this);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        objectPool.ObjectCreated -= OnHairPinObjectCreated;
    }

}
