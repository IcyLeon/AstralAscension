using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacterAnimationEvents : CharacterAnimationEvents
{
    protected PlayableCharacters playableCharacters
    {
        get
        {
            return character as PlayableCharacters;
        }
    }
}
