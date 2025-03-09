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
    private EnhancementManager enhancementManager;
    private Coroutine upgradingAnimationCoroutine;
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
        enhancementManager.OnEnhanceItemChanged += EnhancementManager_OnEnhanceItemChanged;
        enhancementManager.OnSlotChanged += EnhancementManager_OnSlotChanged;
        enhancementManager.OnItemUpgradeProcessing += EnhancementManager_OnItemUpgradeProcessing;
        UpdateVisual();
    }

    private void EnhancementManager_OnItemUpgradeProcessing(object sender, UpgradeEvent e)
    {
        if (upgradingAnimationCoroutine != null)
        {
            StopCoroutine(upgradingAnimationCoroutine);
        }

        upgradingAnimationCoroutine = StartCoroutine(UpgradingCoroutineAnimation(e));
    }

    private IEnumerator UpgradingCoroutineAnimation(UpgradeEvent e)
    {
        float elapseTime = 0f;
        float duration = e.duration;

        while (elapseTime <= duration)
        {
            if (GetCurrentEXP() >= GetMaxCurrentEXP())
            {
                SetCurrentEXP(0f);
                e.totalEXP -= GetMaxCurrentEXP();
                e.currentLevel++;
                int requiredEXP = GetEXPRequired(e.currentLevel);
                SetMaxCurrentEXP(requiredEXP);
                SetPreviewEXP(e.totalEXP);
            }

            SetCurrentEXP(Mathf.Lerp(CurrentExpSlider.value, e.totalEXP, elapseTime / duration));
            elapseTime += Time.unscaledDeltaTime;
            yield return null;
        }

        UpdateItemInformationDisplay();
        upgradingAnimationCoroutine = null;
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


    private int GetCurrentEXP()
    {
        return ((int)CurrentExpSlider.value);
    }

    private void SetMaxCurrentEXP(float value)
    {
        PreviewExpSlider.maxValue = value;
        CurrentExpSlider.maxValue = PreviewExpSlider.maxValue;
    }
    private int GetMaxCurrentEXP()
    {
        return ((int)CurrentExpSlider.maxValue);
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
            enhancementManager.OnEnhanceItemChanged -= EnhancementManager_OnEnhanceItemChanged;
            enhancementManager.OnSlotChanged -= EnhancementManager_OnSlotChanged;
        }
    }

    private void UpdateEXPEntityDisplay()
    {
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
