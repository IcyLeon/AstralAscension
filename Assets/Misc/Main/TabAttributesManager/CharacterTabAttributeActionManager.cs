using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTabAttributeActionManager : MonoBehaviour
{
    public enum TAB_ATTRIBUTE
    {
        ATTRIBUTES,
        ARTIFACTS,
        PROFILE
    }

    [SerializeField] private CharacterScreenPanel CharacterScreenPanel;
    public CameraPanManager cameraPanManager { get; private set; }
    private Dictionary<TAB_ATTRIBUTE, CharacterTabAttributeAction> tabActionDic = new();
    private CharacterTabAttributeAction currentCharacterTabAttributeAction;

    private void Awake()
    {
        cameraPanManager = GetComponentInChildren<CameraPanManager>();
        SetupActionDic();
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        TabAttributesMiscEvent.OnTabSwitch += TabAttributesMiscEvent_OnTabSwitch;
        TabAttributesMiscEvent.OnTabReset += TabAttributesMiscEvent_OnTabReset;
    }

    private void TabAttributesMiscEvent_OnTabReset()
    {
        if (currentCharacterTabAttributeAction == null)
            return;

        ChangeTabAttributeAction(currentCharacterTabAttributeAction);
    }

    private void OnDisable()
    {
        TabAttributesMiscEvent.OnTabSwitch -= TabAttributesMiscEvent_OnTabSwitch;
        TabAttributesMiscEvent.OnTabReset -= TabAttributesMiscEvent_OnTabReset;
    }

    private void TabAttributesMiscEvent_OnTabSwitch(TAB_ATTRIBUTE TabAttribute)
    {
        CharacterTabAttributeAction characterTabAttributeAction = GetCharacterTabAttributeAction(TabAttribute);

        if (characterTabAttributeAction == currentCharacterTabAttributeAction)
            return;

        ChangeTabAttributeAction(characterTabAttributeAction);
    }

    private void SetupActionDic()
    {
        CharacterTabAttributeAction[] CharacterTabAttributeActionList = GetComponentsInChildren<CharacterTabAttributeAction>(true);

        foreach (var CharacterTabAttributeAction in CharacterTabAttributeActionList)
        {
            TAB_ATTRIBUTE TabAttribute = CharacterTabAttributeAction.TabAttribute;

            if (GetCharacterTabAttributeAction(TabAttribute))
                continue;

            CharacterTabAttributeAction.SetScreenPanel(CharacterScreenPanel);
            tabActionDic.Add(TabAttribute, CharacterTabAttributeAction);
        }
    }

    private CharacterTabAttributeAction GetCharacterTabAttributeAction(TAB_ATTRIBUTE TabAttribute)
    {
        if (tabActionDic.TryGetValue(TabAttribute, out CharacterTabAttributeAction CharacterTabAttributeAction))
            return CharacterTabAttributeAction;

        return null;
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
    }
}
