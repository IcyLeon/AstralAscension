using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarkerSelectedMapIcon : CurrentSelectMapIcon
{
    [SerializeField] private TMP_InputField PinField;
    [SerializeField] private TextMeshProUGUI TotalPinText;

    private PinSlot[] PinSlotsList;
    private MarkerPinContent[] MarkerPinContentList;

    public override void Init()
    {
        base.Init();

        if (PinSlotsList != null)
            return;

        PinSlotsList = GetComponentsInChildren<PinSlot>();

        MarkerPinContentList = GetComponentsInChildren<MarkerPinContent>(true);

        foreach (var pinSlot in PinSlotsList)
        {
            pinSlot.PinSlotClick += PinSlot_PinSlotClick;
        }

        foreach (var MarkerPinContent in MarkerPinContentList)
        {
            MarkerPinContent.Init();
        }

        PinField.onValueChanged.AddListener(delegate
        {
            OnPinValueChange();
        });

        Subscribe_OnMarkerAdd();
    }

    private void OnMarkerMapIconChanged(object sender, System.EventArgs e)
    {
        UpdatePinFieldVisual();
    }

    private void OnMapIconAdd(MapIcon mapIcon)
    {
        if (!IsVisible())
            return;

        UpdatePinSelected();
    }

    private void OnPinValueChange()
    {
        if (mapIcon == null || mapIcon.mapObject == null)
            return;

        mapIcon.mapObject.mapIconData.SetMarkerName(PinField.text);
    }

    private void OnDisable()
    {
        RemoveUnconfirmPlacement();
    }

    public void RemoveUnconfirmPlacement()
    {
        if (mapIcon == null)
            return;

        MapIconData MapIconData = mapIcon.mapObject.mapIconData;

        if (MapIconData == null || MapIconData.IsConfirmedPlaced())
            return;

        MapIconData.mapObject.DestroyMapObject();
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

    private void UpdatePinFieldVisual()
    {
        if (mapIcon == null || mapIcon.mapObject == null)
            return;

        PinField.text = mapIcon.mapObject.mapIconData.mapIconName;
    }

    private void UpdatePinSelected()
    {
        if (PinSlotsList.Length == 0)
            return;

        OnSelectedPinSlot(PinSlotsList[0]);
    }

    private void Subscribe_OnMarkerAdd()
    {
        if (mapPopupPanel == null || mapPopupPanel.mapUI == null)
            return;

        mapPopupPanel.OnMapIconChanged += OnMarkerMapIconChanged;
        mapPopupPanel.mapUI.worldMapBackground.OnMapIconAdd += OnMapIconAdd;
        UpdatePinSelected();
        UpdatePinFieldVisual();
    }

    private void Unsubscribe_OnMarkerAdd()
    {
        if (mapPopupPanel == null || mapPopupPanel.mapUI == null)
            return;

        mapPopupPanel.OnMapIconChanged -= OnMarkerMapIconChanged;
        mapPopupPanel.mapUI.worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
    }


    private void PinSlot_PinSlotClick(object sender, System.EventArgs e)
    {
        OnSelectedPinSlot(sender as PinSlot);
    }

    private void OnSelectedPinSlot(PinSlot pinSlot)
    {
        if (pinSlot == null || mapIcon.mapObject is not PlayerMarkerWorldObject)
            return;

        mapIcon.mapObject.mapIconData.SetMarkerSprite(pinSlot.SlotIconTypeSO.IconSprite);
    }

    protected override bool IsVisible()
    {
        return mapIcon.mapIconAction is PlayerMarkerMapIconAction;
    }

    protected override void UpdateInformation()
    {
        WorldMapManager worldMap = mapIcon.worldMapBackground.worldMap;

        if (worldMap == null)
            return;

        TotalPinText.text = worldMap.CountAllPlacedMarkers() + "/" + WorldMapManager.MAX_PIN;
    }
}
