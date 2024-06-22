using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingReuseableData : SwordReuseableData
{
    private ObjectPool<HairpinTeleporter> objectPool;
    public AimRigController aimRigController { get; private set; }

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

    private void HT_OnHairPinHide()
    {
        playableCharacterStateMachine.playableCharacters.playableCharacterDataStat.ResetElementalSkillCooldown();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public KeqingReuseableData(int TotalAttackPhase, CharacterStateMachine characterStateMachine) : base(TotalAttackPhase, characterStateMachine)
    {
        targetPosition = Vector3.zero;

        aimRigController = characterStateMachine.characters.GetComponentInChildren<AimRigController>();

        objectPool = new ObjectPool<HairpinTeleporter>("Prefabs/Characters/PlayableCharacters/Keqing/HairpinTeleporter", characterStateMachine.characters.transform);
        objectPool.CallbackPoolObject((HT, i) =>
        {
            HT.Init(playableCharacterStateMachine.playableCharacters, playableCharacterStateMachine.playableCharacters.transform);
            HT.OnHairPinHide += HT_OnHairPinHide;
        });
    }
}
