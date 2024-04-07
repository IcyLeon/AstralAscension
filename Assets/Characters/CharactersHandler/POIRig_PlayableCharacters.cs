using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_PlayableCharacters : POIRig
{
    public PlayableCharacters playableCharacters
    {
        get
        {
            return (PlayableCharacters)characters;
        }
    }

    protected override bool CanMoveHead()
    {
        return !playableCharacters.PlayableCharacterStateMachine.IsSkillCasting();
    }
}
