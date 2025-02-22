using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBubbleTabSelectedController : ArtifactActionBaseController
{
    private ItemTypeTabGroup itemTypeTabGroup;
    private ArtifactBubble currentArtifactBubble;

    public ArtifactBubbleTabSelectedController(ArtifactPanelController ArtifactPanelController) : base(ArtifactPanelController)
    {
        artifactBubbleManager.OnArtifactBubbleSelected += ArtifactAction_OnArtifactBubbleSelected;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        artifactBubbleManager.EnableSelectedBubble();
        artifactPanelController.artifactAction.DisableRotation();
    }

    public override void OnExit()
    {
        base.OnExit();
        artifactBubbleManager.DisableSelectedBubble();
    }

    protected override void OnSubscribeEvents()
    {
        base.OnSubscribeEvents();

        artifactPanelController.OnPanelClose += ArtifactInformationUI_OnClose;
        artifactPanelController.OnPanelClose += VisibleAllArtifactBubble;
        artifactBubbleManager.OnArtifactBubbleSelected += ItemTypeTabGroup_OnArtifactBubbleSelected;
        SubscribeTabGroupEvents();

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        artifactBubbleManager.OnArtifactBubbleSelected -= ArtifactAction_OnArtifactBubbleSelected;
    }

    protected override void OnUnsubscribeEvents()
    {
        base.OnUnsubscribeEvents();

        artifactPanelController.OnPanelClose -= ArtifactInformationUI_OnClose;
        artifactPanelController.OnPanelClose -= VisibleAllArtifactBubble;
        artifactBubbleManager.OnArtifactBubbleSelected -= ItemTypeTabGroup_OnArtifactBubbleSelected;
        UnsubscribeTabGroupEvents();
    }

    private void VisibleAllArtifactBubble()
    {
        artifactBubbleManager.ToggleAllArtifactBubble(true);
    }

    private void ArtifactInformationUI_OnClose()
    {
        artifactPanelController.ChangeController(artifactPanelController.artifactBubbleSelectionController);
    }


    private void ItemTypeTabGroup_OnItemTypeTabOptionSelect(ItemTypeTabOption ItemTypeTabOption)
    {
        artifactBubbleManager.SelectArtifactBubble(ItemTypeTabOption.ItemTypeSO);
    }

    private void ArtifactAction_OnArtifactBubbleSelected(ArtifactBubble ArtifactBubble)
    {
        currentArtifactBubble = ArtifactBubble;
    }

    private void ItemTypeTabGroup_OnArtifactBubbleSelected(ArtifactBubble ArtifactBubble)
    {
        if (currentArtifactBubble == null)
            return;

        itemTypeTabGroup.SelectItemTypeTabOption(currentArtifactBubble.ArtifactTypeSO);
    }

    private void SubscribeTabGroupEvents()
    {
        itemTypeTabGroup = artifactPanelController.itemTypeTabGroup;

        if (itemTypeTabGroup == null)
            return;

        itemTypeTabGroup.OnItemTypeTabOptionSelect += ItemTypeTabGroup_OnItemTypeTabOptionSelect;

        itemTypeTabGroup.SelectItemTypeTabOption(currentArtifactBubble.ArtifactTypeSO);
    }

    private void UnsubscribeTabGroupEvents()
    {
        if (itemTypeTabGroup == null)
            return;

        itemTypeTabGroup.OnItemTypeTabOptionSelect -= ItemTypeTabGroup_OnItemTypeTabOptionSelect;
    }
}
