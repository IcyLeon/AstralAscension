using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPanelController
{
    private ArtifactAction artifactAction;
    private ArtifactInformation artifactInformationUI;
    private ItemTypeTabGroup itemTypeTabGroup;
    public ArtifactPanelController(ArtifactAction ArtifactAction, CharacterScreenPanel CharacterScreenPanel)
    {
        artifactAction = ArtifactAction;
        artifactInformationUI = CharacterScreenPanel.GetComponentInChildren<ArtifactInformation>(true);
        artifactAction.OnArtifactBubbleSelected += ArtifactAction_OnArtifactBubbleSelected;
    }

    private void InitTabGroup()
    {
        if (itemTypeTabGroup != null)
            return;

        itemTypeTabGroup = artifactInformationUI.itemTypeTabGroup;
        SubscribeTabGroupEvents();
    }

    private void SubscribeTabGroupEvents()
    {
        if (itemTypeTabGroup == null)
            return;

        itemTypeTabGroup.OnItemTypeTabOptionSelect += ItemTypeTabGroup_OnItemTypeTabOptionSelect;
    }

    private void UnsubscribeTabGroupEvents()
    {
        if (itemTypeTabGroup == null)
            return;

        itemTypeTabGroup.OnItemTypeTabOptionSelect -= ItemTypeTabGroup_OnItemTypeTabOptionSelect;
    }

    private void ItemTypeTabGroup_OnItemTypeTabOptionSelect(ItemTypeTabOption ItemTypeTabOption)
    {
        artifactAction.SelectArtifactBubble(ItemTypeTabOption.ItemTypeSO);
    }


    private void ArtifactAction_OnArtifactBubbleSelected(ArtifactBubble ArtifactBubble)
    {
        artifactInformationUI.OpenPanel();

        InitTabGroup();

        if (itemTypeTabGroup == null)
            return;

        itemTypeTabGroup.SelectItemTypeTabOption(ArtifactBubble.ArtifactTypeSO);
    }

    public void OnDestroy()
    {
        UnsubscribeTabGroupEvents();
        artifactAction.OnArtifactBubbleSelected -= ArtifactAction_OnArtifactBubbleSelected;
    }
}
