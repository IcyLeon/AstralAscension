using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordCharacterStateMachine : PlayableCharacterStateMachine
{
    public PlayableCharacterSwordHitCollider SwordHitCollider { get; private set; }

    protected override void InitComponent()
    {
        base.InitComponent();
        SwordHitCollider = characters.GetComponentInChildren<PlayableCharacterSwordHitCollider>();
    }

    protected override void InitSkills()
    {
    }

    public SwordCharacterStateMachine(Characters characters) : base(characters)
    {
    }
}
