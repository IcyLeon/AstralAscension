using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : DamageableCharacters
{
    protected override CharacterStateMachine CreateCharacterStateMachine()
    {
        return new BaseEnemyStateMachine(this);
    }
}
