using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutfitManager.Display
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Button))]
    public class PurchaseTest : MonoBehaviour
    {
        private OutfitAttributesPanel outfitAttributesPanel;
        private SkinSO skinSO;
        private Button button;

        private void Awake()
        {
            outfitAttributesPanel = GetComponentInParent<OutfitAttributesPanel>();

            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnEnable()
        {
            OutfitMiscEvent.OnSkinSelected += OutfitMiscEvent_OnSkinSelected;
        }

        private void OnButtonClick()
        {
            SkinStorage skinStorage = outfitAttributesPanel.skinStorage;
            skinStorage.OwnSkin(skinSO);
        }

        private void OutfitMiscEvent_OnSkinSelected(SkinSO SkinSO)
        { 
            skinSO = SkinSO;
        }

        private void OnDisable()
        {
            OutfitMiscEvent.OnSkinSelected -= OutfitMiscEvent_OnSkinSelected;
        }
    }
}