using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplayManager : MonoBehaviour
{
    [SerializeField] private CharacterSelection CharacterSelection;
    public static CharacterDisplayManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    public void SetScreenPanel(CharacterScreenPanel Panel)
    {
        if (CharacterSelection == null)
        {
            Debug.Log("CharacterSelection not found in CharacterDisplayManager!");
            return;
        }

        CharacterSelection.SetScreenPanel(Panel);
    }
}
