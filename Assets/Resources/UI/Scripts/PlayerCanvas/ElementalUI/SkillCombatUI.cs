using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class SkillCombatUI : MonoBehaviour
{
    [SerializeField] protected CanvasGroup BackgroundCanvasGroup;
    [SerializeField] private TextMeshProUGUI TimerTxt;
    [SerializeField] private Image SkillIconImage;


    public PlayableCharacterDataStat currentPlayableCharacterData { get; private set; }

    protected virtual void Awake()
    {
        CharacterSwitchEvent.OnCharacterSwitch += CharacterSwitchEvent_OnCharacterSwitch;
    }

    private void CharacterSwitchEvent_OnCharacterSwitch(PartyMember PartyMember)
    {
        currentPlayableCharacterData = PartyMember.characterDataStat as PlayableCharacterDataStat;
        UpdateVisuals();
    }

    protected virtual void UpdateVisuals()
    {
        UpdateIcon();
    }

    private void Update()
    {
        UpdateTimer();
        UpdateUsageAlpha();
    }

    private void UpdateIcon()
    {
        if (currentPlayableCharacterData == null || SkillIconImage == null)
            return;

        SkillIconImage.sprite = GetSkillIcon();
    }

    private void UpdateTimer()
    {
        if (TimerTxt == null)
            return;

        TimerTxt.gameObject.SetActive(currentPlayableCharacterData != null && IsInCountdown());
        TimerTxt.text = GetTimerText() + "s";
    }

    protected abstract void UpdateUsageAlpha();
    protected abstract Sprite GetSkillIcon();
    protected abstract bool IsInCountdown();
    protected abstract bool CanUseSkill();
    protected abstract string GetTimerText();

    protected virtual void OnDestroy()
    {
        CharacterSwitchEvent.OnCharacterSwitch -= CharacterSwitchEvent_OnCharacterSwitch;
    }
}
