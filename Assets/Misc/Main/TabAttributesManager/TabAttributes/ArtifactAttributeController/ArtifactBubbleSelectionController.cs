using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactBubbleSelectionController : ArtifactActionBaseController
{
    private ArtifactSphereMouseMotion artifactSphereMouseMotion;
    public ArtifactBubbleSelectionController(ArtifactPanelController ArtifactPanelController) : base(ArtifactPanelController)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();

        artifactPanelController.artifactAction.EnableRotation();
        artifactBubbleManager.EnableSelectedBubble();
        EnableArtifactSphereMouseMotion();
    }

    private void EnableArtifactSphereMouseMotion()
    {
        if (artifactSphereMouseMotion == null)
        {
            artifactSphereMouseMotion = artifactBubbleManager.AddComponent<ArtifactSphereMouseMotion>();
            return;
        }
        artifactSphereMouseMotion.enabled = true;
    }

    private void DisableArtifactSphereMouseMotion()
    {
        if (artifactSphereMouseMotion == null)
        {
            return;
        }
        artifactSphereMouseMotion.enabled = false;
    }

    public override void OnExit()
    {
        base.OnExit();

        DisableArtifactSphereMouseMotion();

        artifactBubbleManager.DisableSelectedBubble();
    }

    protected override void OnSubscribeEvents()
    {
        base.OnSubscribeEvents();

        artifactBubbleManager.OnArtifactBubbleSelected += ArtifactAction_OnArtifactBubbleSelected;
    }

    private void ArtifactAction_OnArtifactBubbleSelected(ArtifactBubble ArtifactBubble)
    {
        artifactPanelController.OpenPanel();

        artifactPanelController.ChangeController(artifactPanelController.artifactBubbleTabSelectedController);
    }

    protected override void OnUnsubscribeEvents()
    {
        base.OnUnsubscribeEvents();

        artifactBubbleManager.OnArtifactBubbleSelected -= ArtifactAction_OnArtifactBubbleSelected;
    }
}
