using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OwnedCharacterIconButton : MonoBehaviour
{
    [SerializeField] private Image ImageIcon;
    private Graphic mainBackgroundGraphic;
    private Toggle toggle;
    private PlayerCharactersSO playerCharactersSO;
    public event Action<PlayerCharactersSO> OnCharacterIconSelected;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
        mainBackgroundGraphic = toggle.targetGraphic;
        toggle.group = GetComponentInParent<ToggleGroup>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });

        ToggleValueChanged(toggle);
    }

    private void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            OnSelectedIcon();
            OnCharacterIconSelected?.Invoke(playerCharactersSO);
            return;
        }

        OnDeselectedIcon();
    }

    private void OnDeselectedIcon()
    {
        var Color = mainBackgroundGraphic.color;
        Color.a = 0.25f;
        mainBackgroundGraphic.color = Color;
    }

    private void OnSelectedIcon()
    {
        var Color = mainBackgroundGraphic.color;
        Color.a = 1f;
        mainBackgroundGraphic.color = Color;
    }

    public void SetPlayableCharacterSO(CharactersSO CharactersSO)
    {
        playerCharactersSO = CharactersSO as PlayerCharactersSO;

        if (playerCharactersSO == null)
            return;

        ImageIcon.sprite = playerCharactersSO.PartyCharacterIcon;
    }
}
