using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class EnhanceStatsPanel : MonoBehaviour
{
    [Header("Upgradable Item Content")]
    [SerializeField] private Slider ExpSlider;
    [SerializeField] private Slider PreviewExpSlider;
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private TextMeshProUGUI AddPreviewLevelTxt;
    [SerializeField] private TextMeshProUGUI ExpRequirementTxt;
    [SerializeField] private TextMeshProUGUI AddPreviewExpTxt;

    private EnhancementManager enhancementManager;

    private IEXP iEXPEntity;
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
        enhancementManager.OnSlotItemChanged += EnhancementManager_OnSlotItemChanged;

        UpdateVisual();
    }

    private void EnhancementManager_OnSlotItemChanged(int Exp)
    {
        SetPreviewEXP(Exp);
        UpdateIncreasedLevel(Exp);
        UpdateIncreasedExp(Exp);
    }

    private void UpdateIncreasedLevel(int AddExp)
    {
        if (iEXPEntity == null)
            return;

        int levelIncrease = enhancementManager.GetIncreasedPreviewLevel(AddExp);

        AddPreviewLevelTxt.gameObject.SetActive(levelIncrease != 0);
        AddPreviewLevelTxt.text = "+" + levelIncrease;
    }

    private void UpdateIncreasedExp(int addExp)
    {
        if (iEXPEntity == null)
            return;

        AddPreviewExpTxt.gameObject.SetActive(addExp != 0);
        AddPreviewExpTxt.text = "+" + addExp.ToString();
    }

    public void SetCurrentEXP(float value)
    {
        ExpSlider.value = value;
    }

    private void ResetEXP()
    {
        SetCurrentEXP(0);
    }

    public float GetCurrentEXP()
    {
        return ExpSlider.value;
    }

    private float GetRequiredEXP()
    {
        return ExpSlider.maxValue;
    }

    private void UpdateRequiredEXP()
    {
        if (iEXPEntity == null || iEXPEntity.GetExpCostSO() == null)
            return;

        ExpSlider.maxValue = enhancementManager.requiredEXP;
    }

    private void SetPreviewEXP(int AddExp = 0)
    {
        if (iEXPEntity == null)
            return;

        PreviewExpSlider.maxValue = GetRequiredEXP();

        PreviewExpSlider.value = AddExp + iEXPEntity.GetCurrentExp();
    }

    private void EnhancementManager_OnEnhanceItemChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (iEXPEntity == enhancementManager.iEXPEntity)
            return;

        UnsubscribeEvents();
        iEXPEntity = enhancementManager.iEXPEntity;
        SubscribeEvents();

        UpdateRequiredEXP();
        SetCurrentEXP(iEXPEntity.GetCurrentExp());
        SetPreviewEXP();

        UpdateItemInformationDisplay();
    }

    private void SubscribeEvents()
    {
        if (iEXPEntity == null)
            return;

        iEXPEntity.OnUpgradeIEXP += IEXPEntity_OnUpgradeIEXP;
    }

    private void IEXPEntity_OnUpgradeIEXP()
    {
        ResetEXP();
        UpdateRequiredEXP();
        SetPreviewEXP();
    }

    private void EnhancementManager_OnItemUpgrade()
    {
        UpdateItemInformationDisplay();
    }

    private void UnsubscribeEvents()
    {
        if (iEXPEntity == null)
            return;

        iEXPEntity.OnUpgradeIEXP -= IEXPEntity_OnUpgradeIEXP;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        if (enhancementManager != null)
        {
            enhancementManager.OnEnhanceItemChanged -= EnhancementManager_OnItemUpgrade;
            enhancementManager.OnEnhanceItemChanged -= EnhancementManager_OnEnhanceItemChanged;
            enhancementManager.OnSlotItemChanged -= EnhancementManager_OnSlotItemChanged;
        }
    }

    private void UpdateEXPEntityDisplay()
    {
        LevelContent.gameObject.SetActive(iEXPEntity != null);

        if (iEXPEntity == null)
            return;

        LevelTxt.text = "+" + iEXPEntity.GetLevel();
        ExpRequirementTxt.text = iEXPEntity.GetCurrentExp() + "/" + enhancementManager.requiredEXP;
    }


    private void UpdateItemInformationDisplay()
    {
        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iEXPEntity.GetIEntity());
        }

        UpdateEXPEntityDisplay();

        ResetPreviewStatsVisual();
    }

    private void ResetPreviewStatsVisual()
    {
        AddPreviewExpTxt.gameObject.SetActive(false);
        AddPreviewLevelTxt.gameObject.SetActive(false);
    }
}
