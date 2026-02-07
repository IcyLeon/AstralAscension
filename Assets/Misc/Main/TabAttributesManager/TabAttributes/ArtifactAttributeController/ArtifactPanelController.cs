using System;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPanelController
{
    public ArtifactAction artifactAction { get; }
    private ArtifactInformation artifactInformationUI;

    public ArtifactActionBaseController artifactBubbleSelectionController;
    public ArtifactActionBaseController artifactBubbleTabSelectedController;
    private List<ArtifactActionBaseController> artifactActionBaseControllerlist;
    private ArtifactActionBaseController currentController;

    public event Action OnPanelClose;

    public ArtifactPanelController(ArtifactAction ArtifactAction, CharacterScreenPanel CharacterScreenPanel)
    {
        artifactActionBaseControllerlist = new();
        artifactAction = ArtifactAction;
        artifactInformationUI = CharacterScreenPanel.GetComponentInChildren<ArtifactInformation>(true);
        artifactInformationUI.OnClose += ArtifactInformationUI_OnClose;

        artifactBubbleSelectionController = new ArtifactBubbleSelectionController(this);
        artifactBubbleTabSelectedController = new ArtifactBubbleTabSelectedController(this);
    }

    public void AddState(ArtifactActionBaseController ArtifactActionBaseController)
    {
        artifactActionBaseControllerlist.Add(ArtifactActionBaseController);
    }

    public ItemTypeTabGroup itemTypeTabGroup
    {
        get
        {
            return artifactInformationUI.itemTypeTabGroup;
        }
    }

    private void ArtifactInformationUI_OnClose()
    {
        OnPanelClose?.Invoke();
    }

    public void OpenPanel()
    {
        artifactInformationUI.OpenPanel();
    }

    public void ChangeController(ArtifactActionBaseController ArtifactActionBaseController)
    {
        if (currentController != null)
        {
            currentController.OnExit();
        }

        currentController = ArtifactActionBaseController;

        if (currentController != null)
        {
            currentController.OnEnter();
        }
    }

    public void ResetControllerState()
    {
        if (currentController == artifactBubbleSelectionController)
            return;

        ChangeController(artifactBubbleSelectionController);
    }

    public void OnDestroy()
    {
        artifactInformationUI.OnClose -= ArtifactInformationUI_OnClose;
        ExitCurrentController();
        DestroyAllController();
    }

    private void ExitCurrentController()
    {
        if (currentController == null)
            return;

        currentController.OnExit();
    }

    private void DestroyAllController()
    {
        if (artifactActionBaseControllerlist == null)
            return;

        for(int i = 0; i < artifactActionBaseControllerlist.Count; i++)
        {
            ArtifactActionBaseController artifactActionBaseController = artifactActionBaseControllerlist[i];

            artifactActionBaseController.OnDestroy();
        }
    }
}
