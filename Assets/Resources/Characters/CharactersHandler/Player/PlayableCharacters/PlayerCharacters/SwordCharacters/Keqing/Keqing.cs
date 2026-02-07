using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    [SerializeField] private ElementsSO test;

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(this, test, 100f);
            TakeDamage(this, playerCharactersSO.ElementSO, 50f);
        }
    }
    protected override PlayableCharacterStateMachine CreatePlayableCharacterStateMachine()
    {
        return new KeqingStateMachine(this);
    }
}
