using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarkerSelectedMapIcon : CurrentSelectMapIcon
{
    private PinSlot[] PinSlotsList;
    private PinSlot currentPinSlotSelected;

    protected override void Awake()
    {
        base.Awake();

        PinSlotsList = GetComponentsInChildren<PinSlot>();

        foreach (var pinSlot in PinSlotsList)
        {
            pinSlot.PinSlotClick += PinSlot_PinSlotClick;
        }
    }

    public override void Init()
    {
        base.Init();

        if (mapPopupPanel == null)
            return;

        mapPopupPanel.OnMapIconChanged += MapPopupPanel_OnMapIconChanged;
    }

    private void MapPopupPanel_OnMapIconChanged(object sender, System.EventArgs e)
    {
        if (PinSlotsList == null || PinSlotsList.Length == 0)
            return;

        OnSelectedPinSlot(PinSlotsList[0]);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (var pinSlot in PinSlotsList)
        {
            pinSlot.PinSlotClick -= PinSlot_PinSlotClick;
        }

        if (mapPopupPanel != null)
        {
            mapPopupPanel.OnMapIconChanged -= MapPopupPanel_OnMapIconChanged;
        }
    }

    private void PinSlot_PinSlotClick(object sender, System.EventArgs e)
    {
        OnSelectedPinSlot(sender as PinSlot);
    }

    private void OnSelectedPinSlot(PinSlot pinSlot)
    {
        currentPinSlotSelected = pinSlot;

        if (currentPinSlotSelected == null || currentPinSlotSelected.SlotIconTypeSO == null)
            return;

        mapIcon.mapIconAction.SetIconSprite(currentPinSlotSelected.SlotIconTypeSO.IconSprite);
    }

    protected override bool IsVisible()
    {
        return mapIcon.mapIconAction is PlayerMarkerMapIconAction;
    }

    protected override void UpdateInformation()
    {
    }
}
