using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAttackStateMachine : StateMachine
{
    protected PlayableCharacterAttackController playableCharacterAttackController;
    public PlayableCharacterStateMachine playableCharacterStateMachine { get; }

    public override void OnDestroy()
    {
        base.OnDestroy();

    }

    public override void OnEnable()
    {
        base.OnEnable();
        if (playableCharacterAttackController == null)
            return;

        playableCharacterAttackController.OnEnable();
    }

    public override void Update()
    {
        base.Update();

        if (playableCharacterAttackController == null)
            return;

        playableCharacterAttackController.Update();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        if (playableCharacterAttackController == null)
            return;

        playableCharacterAttackController.OnDisable();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (playableCharacterAttackController == null)
            return;

        playableCharacterAttackController.FixedUpdate();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();
        if (playableCharacterAttackController == null)
            return;

        playableCharacterAttackController.LateUpdate();
    }

    public PlayableCharacterAttackStateMachine(PlayableCharacterStateMachine PlayableCharacterStateMachine)
    {
        playableCharacterStateMachine = PlayableCharacterStateMachine;
        playableCharacterAttackController = new PlayableCharacterAttackController(this);
    }
}
