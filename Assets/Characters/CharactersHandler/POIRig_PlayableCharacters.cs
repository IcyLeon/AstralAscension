using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_PlayableCharacters : POIRig
{
    [SerializeField] private PlayableCharacters PlayableCharacters;

    private void Start()
    {
        interactReference = PlayableCharacters.player.PlayerInteract;
    }

    protected override bool CanMoveHead()
    {
        return !PlayableCharacters.PlayableCharacterStateMachine.IsSkillCasting();
    }
}
