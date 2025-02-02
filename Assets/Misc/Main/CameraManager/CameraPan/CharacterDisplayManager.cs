using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class CharacterDisplayManager : MonoBehaviour
{
    [SerializeField] private CharacterSelection CharacterSelection;
    [SerializeField] private CharacterTabAttributeActionManager ActionManager;
    public static CharacterDisplayManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void SetScreenPanel(CharacterScreenPanel Panel)
    {
        if (CharacterSelection != null)
        {
            CharacterSelection.SetScreenPanel(Panel);
        }

        if (ActionManager != null)
        {
            ActionManager.SetScreenPanel(Panel);
        }
    }

    public void SetCurrentTabAttributeAction(TabAttributeSO TabAttributeSO)
    {
        ActionManager.ChangeTabAttributeAction(TabAttributeSO);
    }

}
