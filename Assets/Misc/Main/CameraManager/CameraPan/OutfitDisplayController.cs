using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OutfitDisplayController
{
    private ModelsHandler modelsHandler;
    private SkinStorage skinStorage;

    public OutfitDisplayController()
    {
    }

    public void OnEnable()
    {
        OutfitMiscEvent.OnSkinSelected += SetSkinSO;
        CharacterDisplayMiscEvent.OnCharacterShowcase += CharacterDisplayMiscEvent_OnCharacterShowcase;
    }


    private void CharacterDisplayMiscEvent_OnCharacterShowcase(CharactersSO CharactersSO, ModelsHandler ModelsHandler)
    {
        modelsHandler = ModelsHandler;
        skinStorage = CharacterManager.instance.GetSkinStorage(CharactersSO);
        ResetCurrentEquippedSkin();
    }

    public void OnDisable()
    {
        OutfitMiscEvent.OnSkinSelected -= SetSkinSO;
        CharacterDisplayMiscEvent.OnCharacterShowcase -= CharacterDisplayMiscEvent_OnCharacterShowcase;
    }

    public void OnDestroy()
    {
    }

    private void ResetCurrentEquippedSkin()
    {
        if (skinStorage == null)
            return;

        SetSkinSO(skinStorage.currentSkinSO);
    }

    private void SetSkinSO(SkinSO SkinSO)
    {
        if (modelsHandler == null || SkinSO == null)
            return;

        modelsHandler.SetSkinSO(SkinSO);
    }
}
