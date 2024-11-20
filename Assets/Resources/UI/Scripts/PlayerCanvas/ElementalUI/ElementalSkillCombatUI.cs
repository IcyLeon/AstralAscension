using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalSkillCombatUI : SkillCombatUI
{
    protected override bool IsInCountdown()
    {
        if (currentPlayableCharacterData == null)
            return false;

        return currentPlayableCharacterData.IsInElementalSkillCooldown();
    }

    protected override Sprite GetSkillIcon()
    {
        if (currentPlayableCharacterData == null)
            return null;

        return currentPlayableCharacterData.playerCharactersSO.ElementalSkillInfo.SkillSprite;
    }

    protected override string GetTimerText()
    {
        if (currentPlayableCharacterData == null)
            return "";

        return currentPlayableCharacterData.currentElementalSkillCooldownElapsed.ToString("0.0");
    }

    protected override void UpdateUsageAlpha()
    {
        BackgroundCanvasGroup.alpha = CanUseSkill() ? 0.5f : 0.3f;
    }

    protected override bool CanUseSkill()
    {
        if (currentPlayableCharacterData == null)
            return false;

        return currentPlayableCharacterData.CanUseElementalSkill();
    }
}
