using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POIRig_PlayableCharacters : POIRig
{
    [SerializeField] private PlayableCharacters PlayableCharacters;

    private void Awake()
    {
        PlayableCharacters.OnDamageHit += OnDamageHit;
    }
    private void OnDamageHit(IDamageable damageable)
    {
        Debug.Log("Hit");
    }
    private void OnDestroy()
    {
        PlayableCharacters.OnDamageHit -= OnDamageHit;
    }

    private void Start()
    {
        interactReference = PlayableCharacters.player.PlayerInteract;
    }

    protected override bool CanMoveHead()
    {
        return !PlayableCharacters.PlayableCharacterStateMachine.IsSkillCasting();
    }
}
