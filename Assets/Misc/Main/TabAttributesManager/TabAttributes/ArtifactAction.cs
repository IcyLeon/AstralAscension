using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactAction : CharacterTabAttributeAction
{
    [SerializeField] private ArtifactDisplay ArtifactDisplay;
    private ArtifactPanelController artifactPanelController;

    public ArtifactBubbleManager artifactBubbleManager
    {
        get
        {
            return ArtifactDisplay.GetComponentInChildren<ArtifactBubbleManager>(true);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        EnableRotation();
        HideArtifactsBubble();
    }

    public void EnableRotation()
    {
        if (artifactBubbleManager == null)
            return;

        artifactBubbleManager.EnableRotation();
    }

    public void DisableRotation()
    {
        if (artifactBubbleManager == null)
            return;

        artifactBubbleManager.DisableRotation();
    }

    public override void SetScreenPanel(CharacterScreenPanel Panel)
    {
        base.SetScreenPanel(Panel);

        if (artifactPanelController != null)
            return;

        artifactPanelController = new(this, Panel);
    }

    private void ShowArtifactsBubble()
    {
        if (artifactBubbleManager == null)
            return;

        artifactBubbleManager.ShowArtifactsBubble();
    }

    private void HideArtifactsBubble()
    {
        if (artifactBubbleManager == null)
            return;

        artifactBubbleManager.HideArtifactsBubble();
    }


    public override void OnEnter()
    {
        base.OnEnter();
        ShowArtifactsBubble();
        ResetControllerState();
    }

    public override void OnExit()
    {
        base.OnExit();
        HideArtifactsBubble();
        ResetControllerState();
    }

    private void OnDestroy()
    {
        DestroyPanelController();
    }

    private void ResetControllerState()
    {
        if (artifactPanelController == null)
            return;

        artifactPanelController.ChangeController(artifactPanelController.artifactBubbleSelectionController);
    }


    private void DestroyPanelController()
    {
        if (artifactPanelController == null)
            return;

        artifactPanelController.OnDestroy();

    }
}
