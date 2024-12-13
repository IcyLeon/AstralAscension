using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableCharacterStateMachine : CharacterStateMachine
{
    public DamageableCharacters damageableCharacters
    {
        get
        {
            return characters as DamageableCharacters;
        }
    }

    public CharacterDataStat GetCharacterDataStat()
    {
        if (damageableCharacters == null)
            return null;

        return damageableCharacters.GetCharacterDataStat();
    }

    protected DamageableCharacterStateMachine(Characters characters) : base(characters)
    {
    }
}
