using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRigController : MonoBehaviour
{
    [field: SerializeField] public SmoothRigTransition SmoothRigTransition { get; private set; }

    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerAimState.OnPlayerAimEnter += PlayerAimState_OnPlayerAimEnter;
        PlayerAimState.OnPlayerAimExit += PlayerAimState_OnPlayerAimExit;
    }

    protected virtual void PlayerAimState_OnPlayerAimExit(PlayableCharacterStateMachine PCS)
    {
        SmoothRigTransition.SetTargetWeight(0f);
    }

    protected virtual void PlayerAimState_OnPlayerAimEnter(PlayableCharacterStateMachine PCS)
    {
        SmoothRigTransition.SetTargetWeight(1f);
    }

    public void SetTargetPosition(Vector3 position)
    {
        SmoothRigTransition.SetTargetPosition(position);
    }
    private void UnSubscribeEvents()
    {
        PlayerAimState.OnPlayerAimEnter -= PlayerAimState_OnPlayerAimEnter;
        PlayerAimState.OnPlayerAimExit -= PlayerAimState_OnPlayerAimExit;
    }
    private void OnDestroy()
    {
        UnSubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
}
