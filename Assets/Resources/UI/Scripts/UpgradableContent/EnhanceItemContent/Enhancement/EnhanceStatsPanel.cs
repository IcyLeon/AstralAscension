using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

[DisallowMultipleComponent]
public class EnhanceStatsPanel : MonoBehaviour
{
    [Header("Upgradable Item Content")]
    [SerializeField] private Slider CurrentExpSlider;
    [SerializeField] private Slider PreviewExpSlider;
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private TextMeshProUGUI CurrentLevelTxt;
    [SerializeField] private TextMeshProUGUI IncreasePreviewLevelTxt;
    [SerializeField] private TextMeshProUGUI ExpRequirementTxt;
    [SerializeField] private TextMeshProUGUI IncreasePreviewExpTxt;
    private bool toggleStats;
    private EnhancementManager enhancementManager;

    private UpgradableItems upgradableItem;
    private ItemContentInformation[] ItemContentInformations;

    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (ItemContentInformations != null)
            return;

        ItemContentInformations = GetComponentsInChildren<ItemContentInformation>(true);

        enhancementManager = GetComponentInParent<EnhancementManager>();
        enhancementManager.OnItemUpgrade += EnhancementManager_OnItemUpgrade;
        enhancementManager.OnEnhanceItemChanged += EnhancementManager_OnEnhanceItemChanged;
        enhancementManager.OnSlotChanged += EnhancementManager_OnSlotChanged;
        enhancementManager.OnItemUpgradeProcessing += EnhancementManager_OnItemUpgradeProcessing;
        UpdateVisual();
    }

    private void EnhancementManager_OnItemUpgradeProcessing(object sender, UpgradeEvent e)
    {
        toggleStats = false;

        if (GetCurrentEXP() >= GetMaxCurrentEXP())
        {
            SetCurrentEXP(0f);
            e.TotalEXP -= GetMaxCurrentEXP();
            e.currentLevel++;
            int requiredEXP = GetEXPRequired(e.currentLevel);
            SetMaxCurrentEXP(requiredEXP);
            SetPreviewEXP(e.TotalEXP);
        }

        SetCurrentEXP(Mathf.Lerp(CurrentExpSlider.value, e.TotalEXP, e.interpolation));
    }

    private int GetIncreasedPreviewLevel(int IncreaseExp)
    {
        int levelIncrease = 0;
        int totalExp = upgradableItem.currentEXP + IncreaseExp;

        ItemRaritySO itemRaritySO = upgradableItem.GetRaritySO();

        for (int i = upgradableItem.level; i < upgradableItem.expCostManagerSO.GetMaxLevel(itemRaritySO); i++)
        {
            int requiredAmt = upgradableItem.expCostManagerSO.GetRequiredEXP(i, itemRaritySO);
            if (totalExp >= requiredAmt)
            {
                totalExp -= requiredAmt;
                levelIncrease++;
            }
        }

        return levelIncrease;
    }


    private void EnhancementManager_OnSlotChanged(int IncreaseExp)
    {
        if (!toggleStats)
            return;

        int IncreaseLevel = GetIncreasedPreviewLevel(IncreaseExp);
        IncreasePreviewLevelTxt.gameObject.SetActive(IncreaseLevel != 0);
        IncreasePreviewLevelTxt.text = "+" + IncreaseLevel;
        IncreasePreviewExpTxt.gameObject.SetActive(IncreaseExp != 0);
        IncreasePreviewExpTxt.text = "+" + IncreaseExp.ToString();

        SetPreviewEXP(upgradableItem.currentEXP + IncreaseExp);
    }

    private void SetCurrentEXP(float value)
    {
        CurrentExpSlider.value = value;
    }

    private void SetPreviewEXP(float value)
    {
        PreviewExpSlider.value = value;
    }


    public int GetCurrentEXP()
    {
        return ((int)CurrentExpSlider.value);
    }

    private void SetMaxCurrentEXP(float value)
    {
        CurrentExpSlider.maxValue = value;
        SetMaxPreviewEXP(CurrentExpSlider.maxValue);
    }
    private int GetMaxCurrentEXP()
    {
        return ((int)CurrentExpSlider.maxValue);
    }

    private void SetMaxPreviewEXP(float value)
    {
        PreviewExpSlider.maxValue = value;
    }


    private int GetEXPRequired(int level)
    {
        if (upgradableItem == null)
            return 0;

        return upgradableItem.expCostManagerSO.GetRequiredEXP(level, upgradableItem.GetRaritySO());
    }



    private void EnhancementManager_OnEnhanceItemChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (upgradableItem == enhancementManager.upgradableItem)
            return;

        UnsubscribeEvents();
        upgradableItem = enhancementManager.upgradableItem;
        SubscribeEvents();
        UpdateItemInformationDisplay();
    }

    private void SubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnUpgradeIEXP += IEXPEntity_OnUpgradeIEXP;
    }

    private void IEXPEntity_OnUpgradeIEXP()
    {
    }

    private void EnhancementManager_OnItemUpgrade()
    {

        UpdateItemInformationDisplay();
    }

    private void UnsubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnUpgradeIEXP -= IEXPEntity_OnUpgradeIEXP;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        if (enhancementManager != null)
        {
            enhancementManager.OnEnhanceItemChanged -= EnhancementManager_OnItemUpgrade;
            enhancementManager.OnEnhanceItemChanged -= EnhancementManager_OnEnhanceItemChanged;
            enhancementManager.OnItemUpgrade -= EnhancementManager_OnItemUpgrade;
            enhancementManager.OnSlotChanged -= EnhancementManager_OnSlotChanged;
        }
    }

    private void UpdateEXPEntityDisplay()
    {
        toggleStats = true;
        LevelContent.gameObject.SetActive(upgradableItem != null);

        if (upgradableItem == null)
            return;

        int requiredEXP = GetEXPRequired(upgradableItem.level);
        CurrentLevelTxt.text = "+" + upgradableItem.level;
        ExpRequirementTxt.text = upgradableItem.currentEXP + "/" + requiredEXP;

        SetMaxCurrentEXP(requiredEXP);
        SetCurrentEXP(upgradableItem.currentEXP);
        SetPreviewEXP(GetCurrentEXP());
    }


    private void UpdateItemInformationDisplay()
    {
        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(upgradableItem);
        }

        UpdateEXPEntityDisplay();
        ResetPreviewStatsVisual();
    }

    private void ResetPreviewStatsVisual()
    {
        IncreasePreviewExpTxt.gameObject.SetActive(false);
        IncreasePreviewLevelTxt.gameObject.SetActive(false);
    }
}
