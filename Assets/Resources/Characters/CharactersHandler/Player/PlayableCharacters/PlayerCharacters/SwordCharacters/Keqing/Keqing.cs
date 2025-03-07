using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    [SerializeField] ElementsSO test;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            TakeDamage(this, GetElementsSO(), 100f);
            TakeDamage(this, test, 100f);
        }
    }

    
    protected override CharacterStateMachine GetStateMachine()
    {
        return new KeqingStateMachine(this);
    }
}
