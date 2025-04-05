using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAttackController
{
    private const float mouseInputBetweenAction = 0.25f;
    private float mouseInputCurrentElapsed;
    protected PlayableCharacterAttackStateMachine playableCharacterAttackStateMachine;
    protected PlayableCharacterStateMachine playableCharacterStateMachine;

    public PlayableCharacterAttackController(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine)
    {
        playableCharacterAttackStateMachine = PlayableCharacterAttackStateMachine;
        playableCharacterStateMachine = playableCharacterAttackStateMachine.playableCharacterStateMachine;
    }

    public virtual void OnEnable()
    {
        playableCharacterStateMachine.player.playerController.playerInputAction.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (playableCharacterStateMachine.IsSkillCasting() || playableCharacterStateMachine.IsAirborne() ||
            !playableCharacterAttackStateMachine.playableCharacterAttackData.CanAttack())
        {
            return;
        }

        if (Time.time - mouseInputCurrentElapsed >= mouseInputBetweenAction)
        {
            playableCharacterAttackStateMachine.TransitNextAttackState();
            mouseInputCurrentElapsed = Time.time;
        }
    }

    public virtual void OnDisable()
    {
        playableCharacterStateMachine.player.playerController.playerInputAction.Attack.performed -= Attack_performed;
    }


    public virtual void FixedUpdate()
    {
    }

    public virtual void LateUpdate()
    {
    }

 
    public virtual void Update()
    {
    }

    public virtual void OnDestroy()
    {
    }
}
