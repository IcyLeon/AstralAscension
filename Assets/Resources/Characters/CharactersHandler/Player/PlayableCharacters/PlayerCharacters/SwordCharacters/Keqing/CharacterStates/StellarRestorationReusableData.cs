using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarRestorationReusableData : SkillReusableData
{
    private ObjectPool<HairpinTeleporter> objectPool;
    public event EventHandler OnHairPinShoot;

    public Vector3 targetPosition;
    public HairpinTeleporter hairpinTeleporter { get; private set; }

    public void CreateHairpinTeleporter(Transform EmitterPivot)
    {
        hairpinTeleporter = objectPool.GetPooledObject();
        if (hairpinTeleporter == null)
            return;

        hairpinTeleporter.transform.SetParent(EmitterPivot);
        hairpinTeleporter.transform.localPosition = Vector3.zero;
        hairpinTeleporter.SetTargetLocation(targetPosition);
        OnHairPinShoot?.Invoke(this, EventArgs.Empty);
    }
    public float ElementalSkillRange
    {
        get
        {
            return 5f;
        }
    }

    public bool CanThrow()
    {
        return hairpinTeleporter == null || !hairpinTeleporter.CanTeleport();
    }

    private void HT_OnHairPinHide()
    {
        playableCharacterStateMachine.playableCharacter.playableCharacterDataStat.ResetElementalSkillCooldown();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    public StellarRestorationReusableData(SkillStateMachine skill) : base(skill)
    {
        targetPosition = Vector3.zero;

        objectPool = new ObjectPool<HairpinTeleporter>("Characters/CharactersHandler/Player/PlayableCharacters/PlayerCharacters/SwordCharacters/Keqing/TeleporterOrb", playableCharacterStateMachine.characters.transform);
        objectPool.CallbackPoolObject((HT, i) =>
        {
            HT.Init(playableCharacterStateMachine.playableCharacter, playableCharacterStateMachine.playableCharacter.transform);
            HT.OnHairPinHide += HT_OnHairPinHide;
        });
    }
}
