using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    protected CharactersSO charactersSO;
    protected float maxHealth;
    protected float currentHealth;

    public CharacterData(CharactersSO charactersSO)
    {
        this.charactersSO = charactersSO;
    }
}
