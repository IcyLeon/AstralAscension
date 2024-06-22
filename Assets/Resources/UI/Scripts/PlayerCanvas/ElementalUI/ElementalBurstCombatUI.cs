using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PlayableCharacterDataStat;

public class ElementalBurstCombatUI : SkillCombatUI
{
    [SerializeField] private Image EnergyFillImage;

    protected override void Awake()
    {
        base.Awake();
        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
    }

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        PlayableCharacterDataStat PlayableCharacterDataStat = playerData as PlayableCharacterDataStat;
        if (PlayableCharacterDataStat == null)
            return;

        PlayableCharacterDataStat.OnEnergyChanged -= CurrentPlayableCharacterData_OnEnergyChanged;
    }

    protected override void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        base.ActiveCharacter_OnPlayerCharacterSwitch(playerData, playableCharacters);

        if (currentPlayableCharacterData == null)
            return;

        currentPlayableCharacterData.OnEnergyChanged += CurrentPlayableCharacterData_OnEnergyChanged;
        UpdateEnergyVisual();
    }

    private void UpdateEnergyVisual()
    {
        EnergyFillImage.fillAmount = currentPlayableCharacterData.GetEnergyCostRatio();
        EnergyFillImage.gameObject.SetActive(!currentPlayableCharacterData.HasEnoughEnergy());
    }

    protected override void UpdateUsageAlpha()
    {
        if (CanUseSkill())
        {
            BackgroundCanvasGroup.alpha = 1f;
        }
        else
        {
            BackgroundCanvasGroup.alpha = 0.65f;
        }
    }

    private void CurrentPlayableCharacterData_OnEnergyChanged(object sender, System.EventArgs e)
    {
        UpdateEnergyVisual();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;

        if (currentPlayableCharacterData != null)
        {
            currentPlayableCharacterData.OnEnergyChanged -= CurrentPlayableCharacterData_OnEnergyChanged;
        }
    }

    protected override bool IsInCountdown()
    {
        if (currentPlayableCharacterData == null)
            return false;

        return currentPlayableCharacterData.IsInElementalBurstCooldown();
    }

    protected override Sprite GetSkillIcon()
    {
        if (currentPlayableCharacterData == null)
            return null;

        return currentPlayableCharacterData.playerCharactersSO.ElementalBurstInfo.SkillSprite;
    }

    protected override string GetTimerText()
    {
        if (currentPlayableCharacterData == null)
            return "";

        return currentPlayableCharacterData.currentElementalBurstCooldownElapsed.ToString("0.0");
    }

    protected override bool CanUseSkill()
    {
        if (currentPlayableCharacterData == null)
            return false;

        return currentPlayableCharacterData.CanUseElementalBurst();
    }
}
