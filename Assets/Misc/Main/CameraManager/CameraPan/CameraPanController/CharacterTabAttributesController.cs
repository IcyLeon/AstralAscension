using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TabOption))]
[DisallowMultipleComponent]
public class CharacterTabAttributesController : MonoBehaviour
{
    [SerializeField] private TabAttributeSO TabAttributeSO;
    private CharacterDisplayManager characterDisplayManager;
    private TabOption tabOption;

    private void Awake()
    {
        tabOption = GetComponent<TabOption>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        tabOption.OnTabOptionSelect += TabOption_TabOptionSelect;
    }

    private void SubscribeEvents()
    {
        tabOption.OnTabOptionSelect += TabOption_TabOptionSelect;
    }

    private void UnsubscribeEvents()
    {
        tabOption.OnTabOptionSelect -= TabOption_TabOptionSelect;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void TabOption_TabOptionSelect(TabOption tabOption)
    {
        characterDisplayManager = CharacterDisplayManager.instance;

        if (characterDisplayManager == null)
            return;

        characterDisplayManager.SetCurrentTabAttributeAction(TabAttributeSO);
    }
}
