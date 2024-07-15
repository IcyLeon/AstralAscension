using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterDataStat : CharacterDataStat
{
    private Dictionary<ItemTypeSO, UpgradableItems> itemList; // equipped artifacts character
    public float currentElementalSkillCooldownElapsed { get; private set; }
    public float currentElementalBurstCooldownElapsed { get; private set; }

    private float currentEnergy;
    private int currentAscension;

    public event EventHandler OnEnergyChanged;

    public PlayerCharactersSO playerCharactersSO { 
        get 
        { 
            return (PlayerCharactersSO)damageableEntitySO; 
        } 
    }

    public PlayableCharacterDataStat(CharactersSO charactersSO, int currentAscension = 0) : base(charactersSO)
    {
        itemList = new();
        this.currentAscension = currentAscension;
        currentEnergy = 0;
        currentElementalSkillCooldownElapsed = currentElementalBurstCooldownElapsed = 0;
    }

    public void RemoveArtifacts(ItemTypeSO artifactTypeSO)
    {
        Artifact artifact = GetItem(artifactTypeSO) as Artifact;
        if (artifact == null)
            return;

        itemList.Remove(artifactTypeSO);
        artifact.SetEquip(null);
    }

    public Item GetItem(ItemTypeSO itemTypeSO)
    {
        if (itemTypeSO != null && itemList.TryGetValue(itemTypeSO, out UpgradableItems item))
        {
            return item;
        }

        return null;
    }

    public void AddArtifacts(Artifact artifact)
    {
        if (artifact == null)
            return;

        ItemTypeSO itemTypeSO = artifact.GetItemType();

        if (GetItem(itemTypeSO) != null)
        {
            RemoveArtifacts(itemTypeSO);
        }

        itemList[itemTypeSO] = artifact;
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
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateElementalSkillsCooldown()
    {
        currentElementalSkillCooldownElapsed -= Time.deltaTime;
        currentElementalSkillCooldownElapsed = Mathf.Clamp(currentElementalSkillCooldownElapsed, 0, playerCharactersSO.ElementalSkillInfo.SkillCooldown);

        currentElementalBurstCooldownElapsed -= Time.deltaTime;
        currentElementalBurstCooldownElapsed = Mathf.Clamp(currentElementalBurstCooldownElapsed, 0, playerCharactersSO.ElementalBurstInfo.SkillCooldown);
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
