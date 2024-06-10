using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterDataStat : CharacterDataStat
{
    private float currentElementalSkillCooldownElapsed;
    private float currentElementalBurstCooldownElapsed;
    private int currentAscension;

    public PlayerCharactersSO playerCharactersSO { 
        get 
        { 
            return (PlayerCharactersSO)charactersSO; 
        } 
    }

    public PlayableCharacterDataStat(CharactersSO charactersSO, int currentAscension = 0) : base(charactersSO)
    {
        this.currentAscension = currentAscension;
        currentElementalSkillCooldownElapsed = currentElementalBurstCooldownElapsed = 0;
    }

    public override void Update()
    {
        base.Update();
        UpdateElementalSkillCooldown();
    }

    private void UpdateElementalSkillCooldown()
    {
        currentElementalSkillCooldownElapsed -= Time.deltaTime;
        currentElementalSkillCooldownElapsed = Mathf.Clamp(currentElementalSkillCooldownElapsed, 0, playerCharactersSO.ElementalSkillInfo.SkillCooldown);

        currentElementalBurstCooldownElapsed -= Time.deltaTime;
        currentElementalBurstCooldownElapsed = Mathf.Clamp(currentElementalBurstCooldownElapsed, 0, playerCharactersSO.ElementalBurstInfo.SkillCooldown);
    }

    public bool CanUseElementalSkill()
    {
        return currentElementalSkillCooldownElapsed <= 0f;
    }

    public void ResetElementalSkillCooldown()
    {
        currentElementalSkillCooldownElapsed = playerCharactersSO.ElementalSkillInfo.SkillCooldown;
    }

    public bool CanUseElementalBurst()
    {
        return currentElementalBurstCooldownElapsed <= 0f;
    }

    public void ResetElementalBurstCooldown()
    {
        currentElementalBurstCooldownElapsed = playerCharactersSO.ElementalBurstInfo.SkillCooldown;
    }

}
