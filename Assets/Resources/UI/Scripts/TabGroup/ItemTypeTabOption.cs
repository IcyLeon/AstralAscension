using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
[RequireComponent(typeof(TabOption))]
public class ItemTypeTabOption : MonoBehaviour
{
    private TabOption tabOption;
    [field: SerializeField] public ItemTypeSO ItemTypeSO { get; private set; }
    public event Action<ItemTypeTabOption> OnItemTypeTabOptionSelect;

    private void Awake()
    {
        tabOption = GetComponent<TabOption>();
        tabOption.OnTabOptionSelect += TabOption_OnTabOptionSelect;
    }

    private void TabOption_OnTabOptionSelect(TabOption TabOption)
    {
        OnItemTypeTabOptionSelect?.Invoke(this);
    }

    private void OnDestroy()
    {
        tabOption.OnTabOptionSelect -= TabOption_OnTabOptionSelect;
    }
    public void OnClick()
    {
        tabOption.OnClick();
    }

    public Transform GetPanel()
    {
        return tabOption.Panel;
    }
}
