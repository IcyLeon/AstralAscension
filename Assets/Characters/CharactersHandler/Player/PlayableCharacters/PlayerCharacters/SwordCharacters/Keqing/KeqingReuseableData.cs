using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingReuseableData : SwordReuseableData
{
    private ObjectPool<HairpinTeleporter> objectPool;
    public float Range {
        get {
            return 5f;
        }
    }
    public Vector3 targetPosition;
    public HairpinTeleporter hairpinTeleporter;

    public HairpinTeleporter CreateHairpinTeleporter()
    {
        return objectPool.GetPooledObject();
    }

    public bool CanThrow()
    {
        return hairpinTeleporter == null || !hairpinTeleporter.CanTeleport();
    }

    private void OnHairPinObjectCreated(HairpinTeleporter HT)
    {
        if (HT == null)
            return;

        HT.Init(playableCharacterStateMachine.playableCharacters, playableCharacterStateMachine.playableCharacters.transform);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        objectPool.ObjectCreated -= OnHairPinObjectCreated;
    }

    public KeqingReuseableData(int TotalAttackPhase, CharacterStateMachine characterStateMachine) : base(TotalAttackPhase, characterStateMachine)
    {
        targetPosition = Vector3.zero;
        objectPool = new ObjectPool<HairpinTeleporter>("Prefabs/Characters/PlayableCharacters/Keqing/HairpinTeleporter", characterStateMachine.characters.transform);
        objectPool.ObjectCreated += OnHairPinObjectCreated;
    }
}
