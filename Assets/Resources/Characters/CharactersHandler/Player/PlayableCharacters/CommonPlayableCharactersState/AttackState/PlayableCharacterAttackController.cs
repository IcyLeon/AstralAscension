using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAttackController
{
    protected PlayableCharacterAttackStateMachine playableCharacterAttackStateMachine;
    protected PlayableCharacterStateMachine playableCharacterStateMachine;

    public PlayableCharacterAttackController(PlayableCharacterAttackStateMachine PlayableCharacterAttackStateMachine)
    {
        playableCharacterAttackStateMachine = PlayableCharacterAttackStateMachine;
        playableCharacterStateMachine = playableCharacterAttackStateMachine.playableCharacterStateMachine;
    }

    public virtual void OnEnable()
    {
        playableCharacterStateMachine.playerController.playerInputAction.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    public virtual void OnDisable()
    {
        playableCharacterStateMachine.playerController.playerInputAction.Attack.performed -= Attack_performed;
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
}
