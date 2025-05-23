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

    protected CombatUIManager combatUIManager { get; private set; }
    protected Player player;

    public PlayableCharacterDataStat currentPlayableCharacterData { get; private set; }

    protected virtual void Awake()
    {
        combatUIManager = GetComponentInParent<CombatUIManager>();
        combatUIManager.OnPlayerChanged += CombatUIManager_OnPlayerChanged;
    }

    private void CombatUIManager_OnPlayerChanged()
    {
        UnsubscribeEvent();
        player = combatUIManager.player;
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        if (player == null)
            return;

        player.activeCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
        UpdateVisuals();
    }

    private void UnsubscribeEvent()
    {
        if (player == null)
            return;

        player.activeCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }

    protected virtual void UpdateVisuals()
    {
        currentPlayableCharacterData = player.activeCharacter.currentPartyMember.characterDataStat as PlayableCharacterDataStat;
        UpdateIcon();
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(PartyMember PrevPartyMember, PartyMember NewPartyMember)
    {
        UpdateVisuals();
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
        combatUIManager.OnPlayerChanged -= CombatUIManager_OnPlayerChanged;
        UnsubscribeEvent();
    }
}
