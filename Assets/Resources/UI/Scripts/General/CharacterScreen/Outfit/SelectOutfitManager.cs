using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutfitManager.Display
{
    [DisallowMultipleComponent]
    public class SelectOutfitManager : ItemStorageManager
    {
        [SerializeField] private Button switchButton;
        [SerializeField] private GameObject OwnedSkinContent;
        [SerializeField] private GameObject PurchaseSkinContent;
        [SerializeField] private OutfitInfo OutfitInfo;
        private SkinSO currentSelectedSkinSO;
        private OutfitAttributesPanel outfitAttributesPanel;
        private SkinStorage skinStorage;

        private void Awake()
        {
            outfitAttributesPanel = GetComponentInParent<OutfitAttributesPanel>();
            outfitAttributesPanel.OnCharacterChanged += OutfitAttributesPanel_OnCharacterIconSelected;

            switchButton.onClick.AddListener(OnSwitchClick);
        }

        private void OnSwitchClick()
        {
            if (currentSelectedSkinSO == null)
                return;

            OutfitMiscEvent.ApplySkin(currentSelectedSkinSO);
        }

        private void OutfitAttributesPanel_OnCharacterIconSelected()
        {
            UnsubscribeSkinStorageEvents();
            skinStorage = outfitAttributesPanel.skinStorage;
            skinStorage.OnSkinOwned += SkinStorage_OnSkinOwned;
            skinStorage.OnSkinEquipped += SkinStorage_OnSkinEquipped;
            AddSkinsToList();

            SelectItemQuality(skinStorage.currentSkinSO);
        }

        private void SkinStorage_OnSkinEquipped(SkinSO SkinSO)
        {
            ToggleSwitchButton();
        }

        private void ToggleSwitchButton()
        {
            switchButton.interactable = skinStorage.currentSkinSO != currentSelectedSkinSO;
        }

        private void AddSkinsToList()
        {
            if (skinStorage == null)
                return;

            ResetList();

            AddItem(skinStorage.playerCharacterProfileSO.DefaultSkinSO);

            for (int i = 0; i < skinStorage.playerCharacterProfileSO.SkinSOList.Length; i++)
            {
                AddItem(skinStorage.playerCharacterProfileSO.SkinSOList[i]);
            }
        }


        protected override void ItemQuality_OnItemQualitySelect(IData IData)
        {
            base.ItemQuality_OnItemQualitySelect(IData);

            SkinSO SkinSO = IData as SkinSO;

            if (currentSelectedSkinSO == SkinSO)
                return;

            currentSelectedSkinSO = SkinSO;
            DisplayOwnedDetails(skinStorage.IsOwned(currentSelectedSkinSO));
            OutfitMiscEvent.Select(currentSelectedSkinSO);
            ToggleSwitchButton();
        }


        private void SkinStorage_OnSkinOwned(SkinSO SkinSO)
        {
            if (skinStorage == null)
                return;

            DisplayOwnedDetails(skinStorage.IsOwned(SkinSO));
        }

        private void DisplayOwnedDetails(bool toggle)
        {
            OwnedSkinContent.gameObject.SetActive(toggle);
            PurchaseSkinContent.gameObject.SetActive(!toggle);
        }

        private void UnsubscribeSkinStorageEvents()
        {
            if (skinStorage == null)
                return;

            skinStorage.OnSkinOwned -= SkinStorage_OnSkinOwned;
            skinStorage.OnSkinEquipped -= SkinStorage_OnSkinEquipped;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            UnsubscribeSkinStorageEvents();
            outfitAttributesPanel.OnCharacterChanged -= OutfitAttributesPanel_OnCharacterIconSelected;
        }
    }
}