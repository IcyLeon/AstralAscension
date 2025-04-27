using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAttackStateMachine : StateMachine
{
    protected PlayableCharacterAttackController playableCharacterAttackController;
    public PlayableCharacterStateMachine playableCharacterStateMachine { get; }
    public PlayableCharacterIdleAttackState playableCharacterIdleAttackState { get; }
    public PCAttack01State PCAttack01State { get; }
    public PCAttack02State PCAttack02State { get; }
    public PlayableCharacterAttackData playableCharacterAttackData { get; }
    private PlayableCharacterAttackComboState currentAttackComboState;

    public override void OnDestroy()
    {
        base.OnDestroy();

    }

    public override void OnEnable()
    {
        base.OnEnable();

        playableCharacterAttackController.OnEnable();
    }

    public override void Update()
    {
        base.Update();

        playableCharacterAttackData.Update();

        playableCharacterAttackController.Update();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        playableCharacterAttackController.OnDisable();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        playableCharacterAttackController.FixedUpdate();
    }
    public override void LateUpdate()
    {
        base.LateUpdate();

        playableCharacterAttackController.LateUpdate();
    }

    public PlayableCharacterAttackStateMachine(PlayableCharacterStateMachine PlayableCharacterStateMachine)
    {
        playableCharacterStateMachine = PlayableCharacterStateMachine;
        playableCharacterAttackData = new PlayableCharacterAttackData();
        playableCharacterAttackController = new PlayableCharacterAttackController(this);
        playableCharacterIdleAttackState = new PlayableCharacterIdleAttackState(this);
        PCAttack01State = new PCAttack01State(this);
        PCAttack02State = new PCAttack02State(this);
    }

    public void ResetAttackState()
    {
        currentAttackComboState = null;
    }

    public void TransitNextAttackState()
    {
        if (currentAttackComboState == null)
        {
            currentAttackComboState = PCAttack01State;
        }
        else
        {
            currentAttackComboState = currentAttackComboState.GetNextAttackState();
        }

        playableCharacterStateMachine.ChangeState(currentAttackComboState);
    }
}
