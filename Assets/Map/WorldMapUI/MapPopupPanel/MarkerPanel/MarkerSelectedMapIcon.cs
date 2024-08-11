using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarkerSelectedMapIcon : CurrentSelectMapIcon
{
    private PinSlot[] PinSlotsList;
    private PinSlot currentPinSlotSelected;

    public override void Init()
    {
        base.Init();

        if (PinSlotsList != null)
            return;

        PinSlotsList = GetComponentsInChildren<PinSlot>();

        foreach (var pinSlot in PinSlotsList)
        {
            pinSlot.PinSlotClick += PinSlot_PinSlotClick;
        }

        Subscribe_OnMarkerAdd();
    }

    private void WorldMapBackground_OnMapIconAdd(MapIcon mapIcon)
    {
        UpdateVisual();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        foreach (var pinSlot in PinSlotsList)
        {
            pinSlot.PinSlotClick -= PinSlot_PinSlotClick;
        }

        Unsubscribe_OnMarkerAdd();
    }

    private void UpdateVisual()
    {
        if (PinSlotsList.Length == 0)
            return;

        OnSelectedPinSlot(PinSlotsList[0]);
    }

    private void Subscribe_OnMarkerAdd()
    {
        if (mapPopupPanel == null || mapPopupPanel.mapUI == null)
            return;

        mapPopupPanel.mapUI.worldMapBackground.OnMapIconAdd += WorldMapBackground_OnMapIconAdd;
        UpdateVisual();
    }

    private void Unsubscribe_OnMarkerAdd()
    {
        if (mapPopupPanel == null || mapPopupPanel.mapUI == null)
            return;

        mapPopupPanel.mapUI.worldMapBackground.OnMapIconAdd -= WorldMapBackground_OnMapIconAdd;
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

        mapIcon.mapIconAction.mapIconData.SetMapIconTypeSO(currentPinSlotSelected.SlotIconTypeSO);
    }

    protected override bool IsVisible()
    {
        return mapIcon.mapIconAction is PlayerMarkerMapIconAction;
    }

    protected override void UpdateInformation()
    {
    }
}
