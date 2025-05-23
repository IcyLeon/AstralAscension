using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class PlayableCharacterDataStat : CharacterDataStat
{
    public float currentElementalSkillCooldownElapsed { get; private set; }
    public float currentElementalBurstCooldownElapsed { get; private set; }

    private AscensionManager ascensionManager;
    private float currentEnergy;
    public event Action OnEnergyChanged;

    public PlayableCharacterDataStat(CharactersSO charactersSO) : base(charactersSO)
    {
        ascensionManager = new(playerCharactersSO.AscensionSO);
        currentEnergy = 0;
        currentElementalSkillCooldownElapsed = currentElementalBurstCooldownElapsed = 0;
    }

    public PlayerCharactersSO playerCharactersSO
    {
        get
        {
            return damageableEntitySO as PlayerCharactersSO;
        }
    }

    public override void Update()
    {
        base.Update();
        UpdateElementalSkillsCooldown();
    }

    public float GetEnergyCostRatio()
    {
        if (playerCharactersSO == null)
            return 0f;

        return currentEnergy / playerCharactersSO.BurstEnergyCost;
    }

    public void AddEnergy(float amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Min(currentEnergy, playerCharactersSO.BurstEnergyCost);
        OnEnergyChanged?.Invoke();
    }

    private void UpdateElementalSkillsCooldown()
    {
        currentElementalSkillCooldownElapsed = Mathf.Clamp(currentElementalSkillCooldownElapsed - Time.deltaTime, 0, playerCharactersSO.ElementalSkillInfo.SkillCooldown);
        currentElementalBurstCooldownElapsed = Mathf.Clamp(currentElementalBurstCooldownElapsed - Time.deltaTime, 0, playerCharactersSO.ElementalBurstInfo.SkillCooldown);
    }

    public bool CanUseElementalSkill()
    {
        return !IsInElementalSkillCooldown();
    }

    public bool IsInElementalSkillCooldown()
    {
        return currentElementalSkillCooldownElapsed > 0f;
    }

    public void ResetElementalSkillCooldown()
    {
        currentElementalSkillCooldownElapsed = playerCharactersSO.ElementalSkillInfo.SkillCooldown;
    }

    public bool CanUseElementalBurst()
    {
        return !IsInElementalBurstCooldown() && HasEnoughEnergy();
    }

    public bool HasEnoughEnergy()
    {
        return currentEnergy >= playerCharactersSO.BurstEnergyCost;
    }

    public bool IsInElementalBurstCooldown()
    {
        return currentElementalBurstCooldownElapsed > 0f;
    }


    public void ResetElementalBurstCooldown()
    {
        AddEnergy(-currentEnergy);
        currentElementalBurstCooldownElapsed = playerCharactersSO.ElementalBurstInfo.SkillCooldown;
    }
}
