using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactAction : CharacterTabAttributeAction
{
    [SerializeField] private ArtifactDisplay ArtifactDisplay;
    public ArtifactBubbleManager artifactBubbleManager { get; private set; }
    private ArtifactPanelController artifactPanelController;

    protected override void Awake()
    {
        base.Awake();
        artifactBubbleManager = ArtifactDisplay.GetComponentInChildren<ArtifactBubbleManager>(true);
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
        DestroyControllerState();
    }

    private void OnDestroy()
    {
        DestroyPanelController();
    }

    private void ResetControllerState()
    {
        if (artifactPanelController == null)
            return;

        artifactPanelController.ResetControllerState();
    }


    private void DestroyControllerState()
    {
        if (artifactPanelController == null)
            return;

        artifactPanelController.DestroyControllerState();
    }


    private void DestroyPanelController()
    {
        if (artifactPanelController == null)
            return;

        artifactPanelController.OnDestroy();
        DestroyControllerState();

    }
}
