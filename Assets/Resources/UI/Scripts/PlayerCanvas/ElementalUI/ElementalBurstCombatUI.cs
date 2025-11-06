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

    protected override void UpdateVisuals()
    {
        UnsubscribeEvents();
        base.UpdateVisuals();
        SubscribeEvents();
        UpdateEnergyVisual();
    }

    private void SubscribeEvents()
    {
        if (currentPlayableCharacterData == null)
            return;

        currentPlayableCharacterData.OnEnergyChanged += CurrentPlayableCharacterData_OnEnergyChanged;
    }

    private void UnsubscribeEvents()
    {
        if (currentPlayableCharacterData == null)
            return;

        currentPlayableCharacterData.OnEnergyChanged -= CurrentPlayableCharacterData_OnEnergyChanged;
    }


    private void UpdateEnergyVisual()
    {
        if (currentPlayableCharacterData == null)
            return;

        EnergyFillImage.fillAmount = currentPlayableCharacterData.GetEnergyCostRatio();
        EnergyFillImage.gameObject.SetActive(!currentPlayableCharacterData.HasEnoughEnergy());


        if (currentPlayableCharacterData.HasEnoughEnergy())
        {
            backgroundImage.color = Color.white;
            backgroundImage.material = burstMaterial;
            burstMaterial.SetColor("_Color", currentPlayableCharacterData.damageableEntitySO.ElementSO.ColorText);
            return;
        }

        backgroundImage.color = defaultBackgroundColor;
        backgroundImage.material = null;
    }

    protected override void UpdateUsageAlpha()
    {
        if (CanUseSkill())
        {
            BackgroundCanvasGroup.alpha = 1f;
            return;
        }

        BackgroundCanvasGroup.alpha = 0.5f;
    }

    private void CurrentPlayableCharacterData_OnEnergyChanged()
    {
        UpdateEnergyVisual();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        UnsubscribeEvents();
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
