using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : DamageableCharacters
{
    protected override CharacterStateMachine GetStateMachine()
    {
        return new BaseEnemyStateMachine(this);
    }
}
