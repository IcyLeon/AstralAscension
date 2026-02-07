using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class OutfitAttributesPanel : TabAttributesPanel
{
    public SkinStorage skinStorage { get; private set; }
    private CharacterScreenPanel characterScreenPanel;
    public event Action OnCharacterChanged;

    protected override void Awake()
    {
        base.Awake();
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnCharacterIconSelected += CharacterScreenPanel_OnCharacterIconSelected;
    }

    protected override void Start()
    {
        base.Start();
        UpdateSkinStorage();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        OutfitMiscEvent.OnSkinApply += OutfitMiscEvent_OnSkinApply;
        OutfitMiscEvent.OnSkinSelected += OutfitMiscEvent_OnSkinSelected;
        UpdateSkinStorage();
    }

    private void OutfitMiscEvent_OnSkinSelected(SkinSO SkinSO)
    {
        TabAttributesMiscEvent.Reset();
    }

    private void OutfitMiscEvent_OnSkinApply(SkinSO SkinSO)
    {
        if (SkinSO == null)
            return;

        skinStorage.EquipSkin(SkinSO);
        TabAttributesMiscEvent.Reset();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OutfitMiscEvent.OnSkinApply -= OutfitMiscEvent_OnSkinApply;
        OutfitMiscEvent.OnSkinSelected -= OutfitMiscEvent_OnSkinSelected;
        UpdateSkinStorage();
    }

    private void CharacterScreenPanel_OnCharacterIconSelected()
    {
        UpdateSkinStorage();
    }

    private void UpdateSkinStorage()
    {
        skinStorage = CharacterManager.instance.GetSkinStorage(characterScreenPanel.characterEquipmentManager.charactersSO);
        OnCharacterChanged?.Invoke();
    }

    private void OnDestroy()
    {
        characterScreenPanel.OnCharacterIconSelected -= CharacterScreenPanel_OnCharacterIconSelected;
    }
}
