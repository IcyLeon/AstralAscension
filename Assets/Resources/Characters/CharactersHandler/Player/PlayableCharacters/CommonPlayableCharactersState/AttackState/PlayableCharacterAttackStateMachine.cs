using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAttackStateMachine : StateMachine
{
    public PlayableCharacterStateMachine playableCharacterStateMachine { get; }
    public PlayableCharacterAttackData playableCharacterAttackData { get; }

    public override void OnEnable()
    {
        base.OnEnable();
        playableCharacterAttackData.OnEnable();
        playableCharacterStateMachine.player.playerController.playerInputAction.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (playableCharacterStateMachine.IsSkillCasting() || playableCharacterStateMachine.IsAirborne() ||
            !playableCharacterAttackData.CanAttack())
        {
            return;
        }


        TransitFirstAttackState();
    }

    public override void Update()
    {
        base.Update();

        playableCharacterAttackData.Update();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        playableCharacterAttackData.OnDisable();
        playableCharacterStateMachine.player.playerController.playerInputAction.Attack.performed -= Attack_performed;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

    }
    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public PlayableCharacterAttackStateMachine(PlayableCharacterStateMachine PlayableCharacterStateMachine)
    {
        playableCharacterStateMachine = PlayableCharacterStateMachine;
        playableCharacterAttackData = new PlayableCharacterAttackData();
    }

    private bool IsAttacking()
    {
        return playableCharacterStateMachine.IsInState<PlayableCharacterAttackState>();
    }

    public void TransitFirstAttackState()
    {
        if (IsAttacking())
            return;

        playableCharacterStateMachine.ChangeState(new PCAttack01State(this, playableCharacterStateMachine));
    }
}
