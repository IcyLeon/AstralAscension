using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ArtifactAction : CharacterTabAttributeAction
{
    [SerializeField] private ArtifactDisplay ArtifactDisplay;
    private ArtifactSphereMouseMotion artifactSphereMouseMotion;
    private ArtifactBubbleManager artifactBubbleManager;
    private SelectedArtifactBubble selectedArtifactBubble;

    private ArtifactPanelController artifactPanelController;
    public event Action<ArtifactBubble> OnArtifactBubbleSelected;

    protected override void Awake()
    {
        base.Awake();
        artifactBubbleManager = ArtifactDisplay.GetComponentInChildren<ArtifactBubbleManager>();
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

    public override void SetScreenPanel(CharacterScreenPanel Panel)
    {
        base.SetScreenPanel(Panel);
        InitUIController(Panel);
    }

    private void InitUIController(CharacterScreenPanel Panel)
    {
        if (artifactPanelController != null)
            return;

        artifactPanelController = new(this, Panel);
    }

    private void EnableSelectedBubble()
    {
        if (selectedArtifactBubble == null)
        {
            selectedArtifactBubble = artifactBubbleManager.AddComponent<SelectedArtifactBubble>();
            return;
        }
        selectedArtifactBubble.enabled = true;
    }

    private void DisableSelectedBubble()
    {
        if (selectedArtifactBubble == null)
        {
            return;
        }
        selectedArtifactBubble.enabled = false;
    }


    private void SubscribeEvents()
    {
        artifactBubbleManager.OnArtifactBubbleSelected += ArtifactBubbleManager_OnArtifactBubbleSelected;
    }

    private void UnSubscribeEvents()
    {
        artifactBubbleManager.OnArtifactBubbleSelected -= ArtifactBubbleManager_OnArtifactBubbleSelected;
    }


    private void ArtifactBubbleManager_OnArtifactBubbleSelected(ArtifactBubble ArtifactBubble)
    {
        DisableArtifactSphereMouseMotion();
        OnArtifactBubbleSelected?.Invoke(ArtifactBubble);
    }

    public void SelectArtifactBubble(ItemTypeSO ItemTypeSO)
    {
        artifactBubbleManager.SelectArtifactBubble(ItemTypeSO);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        EnableArtifactSphereMouseMotion();
        EnableSelectedBubble();
        SubscribeEvents();
    }

    public override void OnExit()
    {
        base.OnExit();
        DisableArtifactSphereMouseMotion();
        DisableSelectedBubble();
        UnSubscribeEvents();
    }

    private void OnDestroy()
    {
        DestroyPanelController();
    }

    private void DestroyPanelController()
    {
        if (artifactPanelController == null)
            return;

        artifactPanelController.OnDestroy();
    }
}
