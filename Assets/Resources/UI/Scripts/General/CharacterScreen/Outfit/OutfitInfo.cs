using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OutfitInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SkinNameTxt;
    [SerializeField] private TextMeshProUGUI CharacterNameTxt;

    private void Awake()
    {
        OutfitMiscEvent.OnSkinSelected += OutfitMiscEvent_OnSkinSelected;
    }

    private void OnDestroy()
    {
        OutfitMiscEvent.OnSkinSelected -= OutfitMiscEvent_OnSkinSelected;
    }

    private void OutfitMiscEvent_OnSkinSelected(SkinSO SkinSO)
    {
        if (SkinSO == null)
            return;

        SkinNameTxt.text = SkinSO.Name;
        CharacterNameTxt.text = SkinSO.Description;
    }
}
