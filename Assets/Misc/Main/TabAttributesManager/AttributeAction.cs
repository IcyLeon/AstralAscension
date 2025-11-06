using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeAction : CharacterTabAttributeAction
{
    [SerializeField] private CharacterDisplay CharacterDisplay;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        CharacterDisplayMiscEvent.OnCharacterShowcase += CharacterDisplayMiscEvent_OnCharacterShowcase;
    }

    private void CharacterDisplayMiscEvent_OnCharacterShowcase(CharactersSO CharactersSO)
    {
        if (CharacterDisplay.currentCharacterSelected == CharactersSO)
            return;

        ResetCamera();
    }

    public override void OnExit()
    {
        base.OnExit();
        CharacterDisplayMiscEvent.OnCharacterShowcase -= CharacterDisplayMiscEvent_OnCharacterShowcase;
    }
}
