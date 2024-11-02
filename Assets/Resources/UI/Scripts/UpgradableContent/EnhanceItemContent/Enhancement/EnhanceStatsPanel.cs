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

        UpdateIItem();
    }

    private void EnhancementManager_OnSlotItemChanged(object sender, UpgradeEvents e)
    {
        UpdatePreviewExpSliderValue(e.Exp);
        UpdateIncreasedLevel(e.Exp);
        UpdateIncreasedExp(e.Exp);
    }

    private void UpdateIncreasedLevel(int addExp)
    {
        if (iEXPEntity == null)
            return;

        int levelIncrease = GetIncreasedPreviewLevel(iEXPEntity, addExp);

        AddPreviewLevelTxt.gameObject.SetActive(levelIncrease != 0);
        AddPreviewLevelTxt.text = "+" + levelIncrease;
    }

    private int GetIncreasedPreviewLevel(IEXP iExpItem, int addExp)
    {
        int levelIncrease = 0;
        int totalExp = iExpItem.GetCurrentExp() + addExp;

        for (int i = iExpItem.GetLevel(); i < iExpItem.GetExpCostSO().GetMaxLevel(iEXPEntity.GetIEntity().GetRarity()); i++)
        {
            int requiredAmt = iExpItem.GetExpCostSO().GetRequiredEXP(i, iEXPEntity.GetIEntity().GetRarity());
            if (totalExp >= requiredAmt)
            {
                totalExp -= requiredAmt;
                levelIncrease++;
            }
        }

        return levelIncrease;
    }

    private void UpdateIncreasedExp(int addExp)
    {
        if (iEXPEntity == null)
            return;

        AddPreviewExpTxt.gameObject.SetActive(addExp != 0);
        AddPreviewExpTxt.text = "+" + addExp.ToString();
    }

    public void SetExpSliderValue(float value)
    {
        ExpSlider.value = value;
    }

    public float GetExpSliderValue()
    {
        return ExpSlider.value;
    }

    private void UpdatePreviewExpSliderValue(int AddExp = 0)
    {
        if (iEXPEntity == null)
            return;

        PreviewExpSlider.maxValue = ExpSlider.maxValue;

        PreviewExpSlider.value = AddExp + iEXPEntity.GetCurrentExp();
    }

    private void EnhancementManager_OnEnhanceItemChanged(object sender, System.EventArgs e)
    {
        UpdateIItem();
    }

    private void UpdateIItem()
    {
        if (iEXPEntity == enhancementManager.iEXPEntity)
            return;

        Init();
        UnsubscribeIEXPEvents();
        iEXPEntity = enhancementManager.iEXPEntity;
        SubscribeIEXPEvents();
        UpdateVisual();
    }

    private void SubscribeIEXPEvents()
    {
        if (iEXPEntity == null)
            return;

        iEXPEntity.OnUpgradeIEXP += IEXPEntity_OnUpgradeIEXP;

        UpdateExpMaxSliderValue();
        SetExpSliderValue(iEXPEntity.GetCurrentExp());
        UpdatePreviewExpSliderValue();
    }

    private void UpdateExpMaxSliderValue()
    {
        if (iEXPEntity == null || iEXPEntity.GetExpCostSO() == null)
            return;

        ExpSlider.maxValue = iEXPEntity.GetExpCostSO().GetRequiredEXP(iEXPEntity.GetLevel(), iEXPEntity.GetIEntity().GetRarity());
    }

    private void IEXPEntity_OnUpgradeIEXP(object sender, System.EventArgs e)
    {
        ResetEXPSlider();
        UpdateExpMaxSliderValue();
        UpdatePreviewExpSliderValue();
    }

    private void EnhancementManager_OnItemUpgrade(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void ResetEXPSlider()
    {
        SetExpSliderValue(0);
    }

    private void UnsubscribeIEXPEvents()
    {
        if (iEXPEntity == null)
            return;

        iEXPEntity.OnUpgradeIEXP -= IEXPEntity_OnUpgradeIEXP;
    }

    private void OnDestroy()
    {
        UnsubscribeIEXPEvents();
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
        ExpRequirementTxt.text = iEXPEntity.GetCurrentExp() + "/" + iEXPEntity.GetExpCostSO().GetRequiredEXP(iEXPEntity.GetLevel(), iEXPEntity.GetIEntity().GetRarity());
    }


    private void UpdateVisual()
    {
        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iEXPEntity.GetIEntity());
        }

        UpdateEXPEntityDisplay();

        ResetIncreasedPreviewVisual();
    }

    private void ResetIncreasedPreviewVisual()
    {
        AddPreviewExpTxt.gameObject.SetActive(false);
        AddPreviewLevelTxt.gameObject.SetActive(false);
    }
}
