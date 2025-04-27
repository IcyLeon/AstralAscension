using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementalBurstCombatUI : SkillCombatUI
{
    [SerializeField] private Material ElementalBurstMaterial;
    [SerializeField] private Image EnergyFillImage;
    private Material burstMaterial;
    private Image backgroundImage;
    private Color defaultBackgroundColor;

    protected override void Awake()
    {
        base.Awake();
        backgroundImage = BackgroundCanvasGroup.GetComponent<Image>();
        defaultBackgroundColor = backgroundImage.color;

        burstMaterial = Instantiate(ElementalBurstMaterial);
    }

    protected override void OnSubscribeEvent()
    {
        base.OnSubscribeEvent();
        combatUIManager.player.activeCharacter.OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterExit;
    }

    protected override void OnUnsubscribeEvent()
    {
        base.OnUnsubscribeEvent();
        combatUIManager.player.activeCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
    }


    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PartyMember PartyMember)
    {
        PlayableCharacterDataStat PlayableCharacterDataStat = playerData as PlayableCharacterDataStat;
        if (PlayableCharacterDataStat == null)
            return;

        PlayableCharacterDataStat.OnEnergyChanged -= CurrentPlayableCharacterData_OnEnergyChanged;
    }

    protected override void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PartyMember PartyMember)
    {
        base.ActiveCharacter_OnPlayerCharacterSwitch(playerData, PartyMember);

        if (currentPlayableCharacterData == null)
            return;

        currentPlayableCharacterData.OnEnergyChanged += CurrentPlayableCharacterData_OnEnergyChanged;
        UpdateEnergyVisual();
    }

    private void UpdateEnergyVisual()
    {
        EnergyFillImage.fillAmount = currentPlayableCharacterData.GetEnergyCostRatio();
        EnergyFillImage.gameObject.SetActive(!currentPlayableCharacterData.HasEnoughEnergy());


        if (currentPlayableCharacterData.HasEnoughEnergy())
        {
            backgroundImage.color = Color.white;
            backgroundImage.material = burstMaterial;
            burstMaterial.SetColor("_Color", currentPlayableCharacterData.damageableEntitySO.ElementSO.ColorText);
        }
        else
        {
            backgroundImage.color = defaultBackgroundColor;
            backgroundImage.material = null;
        }
    }

    protected override void UpdateUsageAlpha()
    {
        if (CanUseSkill())
        {
            BackgroundCanvasGroup.alpha = 1f;
        }
        else
        {
            BackgroundCanvasGroup.alpha = 0.5f;
        }
    }

    private void CurrentPlayableCharacterData_OnEnergyChanged()
    {
        UpdateEnergyVisual();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

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
