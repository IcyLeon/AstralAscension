using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTabAttributeActionManager : MonoBehaviour
{
    private CameraPanManager cameraPanManager;
    private Dictionary<TabAttributeSO, CharacterTabAttributeAction> tabActionDic;
    private CharacterTabAttributeAction currentCharacterTabAttributeAction;

    private void Awake()
    {
        Init();
        cameraPanManager = GetComponentInChildren<CameraPanManager>();
    }

    private void Start()
    {
        DisableAllTabAttributeActions();
    }

    public void SetScreenPanel(CharacterScreenPanel Panel)
    {
        if (tabActionDic == null)
            return;

        foreach (var CharacterTabAttributeAction in tabActionDic.Values)
        {
            CharacterTabAttributeAction.SetScreenPanel(Panel);
        }
    }

    private void Init()
    {
        tabActionDic = new();

        CharacterTabAttributeAction[] CharacterTabAttributeActionList = GetComponentsInChildren<CharacterTabAttributeAction>(true);

        foreach (var CharacterTabAttributeAction in CharacterTabAttributeActionList)
        {
            TabAttributeSO TabAttributeSO = CharacterTabAttributeAction.TabAttributeSO;

            if (GetCharacterTabAttributeAction(TabAttributeSO))
                continue;

            tabActionDic.Add(TabAttributeSO, CharacterTabAttributeAction);
        }
    }

    private CharacterTabAttributeAction GetCharacterTabAttributeAction(TabAttributeSO TabAttributeSO)
    {
        if (tabActionDic.TryGetValue(TabAttributeSO, out CharacterTabAttributeAction CharacterTabAttributeAction))
            return CharacterTabAttributeAction;

        return null;
    }

    private void DisableAllTabAttributeActions()
    {
        foreach (var CharacterTabAttributeAction in tabActionDic.Values)
        {
            CharacterTabAttributeAction.OnExit();
        }
    }

    public void ChangeTabAttributeAction(TabAttributeSO TabAttributeSO)
    {
        ChangeTabAttributeAction(GetCharacterTabAttributeAction(TabAttributeSO));
    }

    private void ChangeTabAttributeAction(CharacterTabAttributeAction characterTabAttributeAction)
    {
        if (currentCharacterTabAttributeAction != null)
        {
            currentCharacterTabAttributeAction.OnExit();
        }

        currentCharacterTabAttributeAction = characterTabAttributeAction;

        if (currentCharacterTabAttributeAction == null)
            return;

        currentCharacterTabAttributeAction.OnEnter();

        cameraPanManager.ChangeCamera(currentCharacterTabAttributeAction.CameraPanVirtualCam);
    }
}
