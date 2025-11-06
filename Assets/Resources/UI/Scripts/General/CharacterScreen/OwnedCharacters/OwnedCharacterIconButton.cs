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
    public CharacterEquipmentManager characterEquipmentManager { get; private set; }
    public event Action<OwnedCharacterIconButton> OnCharacterIconSelected;

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
            OnCharacterIconSelected?.Invoke(this);
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

    public void SetCharacterEquipmentManager(CharacterEquipmentManager CharacterEquipmentManager)
    {
        characterEquipmentManager = CharacterEquipmentManager;
        ImageIcon.sprite = GetIcon();
    }

    private Sprite GetIcon()
    {
        if (characterEquipmentManager == null)
            return null;

        PlayerCharactersSO PlayerCharactersSO = characterEquipmentManager.charactersSO as PlayerCharactersSO;
        if (PlayerCharactersSO != null)
        {
            return PlayerCharactersSO.PartyCharacterIcon;
        }

        return characterEquipmentManager.charactersSO.GetIcon();
    }
}
