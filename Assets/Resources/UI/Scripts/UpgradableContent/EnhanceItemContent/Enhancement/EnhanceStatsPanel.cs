using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class EnhanceStatsPanel : MonoBehaviour
{
    [Header("Upgradable Item Content")]
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private TextMeshProUGUI ExpTxt;
    private EnhancementManager enhancementManager;

    private IItem iItem;
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
    }

    private void EnhancementManager_OnItemUpgrade(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void OnDestroy()
    {
        if (enhancementManager != null)
        {
            enhancementManager.OnEnhanceItemChanged -= EnhancementManager_OnItemUpgrade;
        }
    }

    public void SetInterfaceItem(IItem IItem)
    {
        if (iItem == IItem)
            return;

        Init();
        iItem = IItem;
        UpdateVisual();
    }

    private void UpdateEXPEntityDisplay()
    {
        IEXP iExpEntity = iItem as IEXP;

        LevelContent.gameObject.SetActive(iExpEntity != null);

        if (iExpEntity == null)
            return;

        LevelTxt.text = "+" + iExpEntity.GetLevel();
        ExpTxt.text = iExpEntity.GetCurrentExp() + "/" + iExpEntity.GetExpCostSO().GetRequiredEXP(iExpEntity.GetLevel(), iItem.GetRarity());
    }


    private void UpdateVisual()
    {
        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iItem);
        }

        UpdateEXPEntityDisplay();

    }
}
