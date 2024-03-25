using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keqing : PlayableCharacters
{
    protected override void Start()
    {
        base.Start();
        characterStateMachine = new KeqingStateMachine(this);
    }
}
