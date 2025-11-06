using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CharacterTabAttributeActionManager;

[RequireComponent(typeof(TabOption))]
[DisallowMultipleComponent]
public class CharacterTabAttributesController : MonoBehaviour
{
    [SerializeField] private TAB_ATTRIBUTE TabAttribute;
    private TabOption tabOption;

    private void Awake()
    {
        tabOption = GetComponent<TabOption>();
    }

    private void OnEnable()
    {
        tabOption.OnTabOptionSelect += TabOption_TabOptionSelect;
    }

    private void OnDisable()
    {
        tabOption.OnTabOptionSelect -= TabOption_TabOptionSelect;
    }
    private void TabOption_TabOptionSelect(TabOption tabOption)
    {
        TabAttributesMiscEvent.Switch(TabAttribute);
    }
}
