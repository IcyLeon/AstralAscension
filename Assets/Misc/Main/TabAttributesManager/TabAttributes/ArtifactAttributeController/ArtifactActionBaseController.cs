using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactActionBaseController
{
    protected ArtifactPanelController artifactPanelController;
    protected ArtifactBubbleManager artifactBubbleManager;

    public ArtifactActionBaseController(ArtifactPanelController ArtifactPanelController)
    {
        artifactPanelController = ArtifactPanelController;
        artifactBubbleManager = artifactPanelController.artifactAction.artifactBubbleManager;

        artifactPanelController.AddState(this);
    }

    public virtual void OnEnter()
    {
        OnSubscribeEvents();
    }

    public virtual void OnExit()
    {
        OnUnsubscribeEvents();
    }


    protected virtual void OnSubscribeEvents()
    {

    }
    protected virtual void OnUnsubscribeEvents()
    {

    }

    public virtual void OnDestroy()
    {
    }
}
