using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSlot : MonoBehaviour
{
    [field: SerializeField] public MapIconTypeSO SlotIconTypeSO { get; private set; }
    [SerializeField] private Image PinSlotImage;
    private Button PinButton;
    public event EventHandler PinSlotClick;

    public void Awake()
    {
        PinButton = GetComponent<Button>();
        PinButton.onClick.AddListener(OnPinClick);
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        PinSlotImage.sprite = SlotIconTypeSO.IconSprite;
    }

    private void OnPinClick()
    {
        PinSlotClick?.Invoke(this, EventArgs.Empty);
    }
}
