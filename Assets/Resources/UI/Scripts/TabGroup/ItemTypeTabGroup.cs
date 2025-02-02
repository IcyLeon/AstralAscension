using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(TabGroup))]
public class ItemTypeTabGroup : MonoBehaviour
{
    private TabGroup tabGroup;
    public ItemTypeTabOption[] itemTypeTabOptions { get; private set; }
    private Dictionary<ItemTypeSO, ItemTypeTabOption> itemTypeTabOptionDic;
    public event Action<ItemTypeTabOption> OnItemTypeTabOptionSelect;

    private void Awake()
    {
        tabGroup = GetComponent<TabGroup>();
    }

    private void Start()
    {
        InitTabOptions();
    }

    private void InitTabOptions()
    {
        if (itemTypeTabOptionDic != null)
            return;

        itemTypeTabOptionDic = new();

        foreach (var TabOption in tabGroup.tabOptions)
        {
            ItemTypeTabOption ItemTypeTabOption = TabOption.GetComponent<ItemTypeTabOption>();

            if (ItemTypeTabOption == null)
                continue;

            ItemTypeTabOption.OnItemTypeTabOptionSelect += ItemTypeTabOption_OnItemTypeTabOptionSelect;
            itemTypeTabOptionDic.Add(ItemTypeTabOption.ItemTypeSO, ItemTypeTabOption);
        }
    }

    private void UnsubscribeEvents()
    {
        foreach (var ItemTypeSO in itemTypeTabOptionDic.Keys)
        {
            itemTypeTabOptionDic[ItemTypeSO].OnItemTypeTabOptionSelect -= ItemTypeTabOption_OnItemTypeTabOptionSelect;
        }

        itemTypeTabOptionDic.Clear();
    }

    public void SelectItemTypeTabOption(ItemTypeSO ItemTypeSO)
    {
        InitTabOptions();

        if (!HasItemTypeSO(ItemTypeSO))
            return;

        itemTypeTabOptionDic[ItemTypeSO].OnClick();
    }

    private bool HasItemTypeSO(ItemTypeSO ItemTypeSO)
    {
        return itemTypeTabOptionDic.ContainsKey(ItemTypeSO);
    }

    private void ItemTypeTabOption_OnItemTypeTabOptionSelect(ItemTypeTabOption ItemTypeTabOption)
    {
        OnItemTypeTabOptionSelect?.Invoke(ItemTypeTabOption);
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
