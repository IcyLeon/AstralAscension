using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingReuseableData : SwordReuseableData
{
    private ObjectPool<HairpinTeleporter> objectPool;
    public Vector3 targetPosition;

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

    private KeqingStateMachine keqingStateMachine
    {
        get
        {
            return characterStateMachine as KeqingStateMachine;
        }
    }

    private void OnHairPinObjectCreated(HairpinTeleporter HT)
    {
        if (HT == null)
            return;

        HT.SetPlayableCharacter(keqingStateMachine.keqing);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        objectPool.ObjectCreated -= OnHairPinObjectCreated;
    }

    public KeqingReuseableData(int TotalAttackPhase, CharacterStateMachine characterStateMachine) : base(TotalAttackPhase, characterStateMachine)
    {
        targetPosition = Vector3.zero;
        objectPool = new ObjectPool<HairpinTeleporter>(keqingStateMachine.keqing.HairpinTeleporterPrefab, characterStateMachine.characters.transform);
        objectPool.ObjectCreated += OnHairPinObjectCreated;
    }
}
