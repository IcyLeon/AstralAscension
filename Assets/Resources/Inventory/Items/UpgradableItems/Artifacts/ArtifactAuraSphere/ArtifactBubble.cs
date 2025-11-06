using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtifactBubble : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI LevelTxt;
    [field: SerializeField] public ItemTypeSO ArtifactTypeSO { get; private set; }
    [SerializeField] private Image PlaceholderImage;
    [SerializeField] private Image ItemImage;
    public event Action<ArtifactBubble> OnArtifactSphereSelect;
    private ArtifactBubbleManager artifactBubbleManager;
    private Artifact artifact;

    private void Awake()
    {
        artifactBubbleManager = GetComponentInParent<ArtifactBubbleManager>();
        artifactBubbleManager.OnArtifactInventoryChanged += ArtifactBubbleManager_OnArtifactInventoryChanged;
        artifactBubbleManager.OnArtifactEquip += ArtifactBubbleManager_OnArtifactEquip;
        artifactBubbleManager.OnArtifactUnEquip += ArtifactBubbleManager_OnArtifactUnEquip;
    }

    private void ArtifactBubbleManager_OnArtifactInventoryChanged()
    {
        ResetArtifact();
    }

    private void ArtifactBubbleManager_OnArtifactUnEquip(Artifact Artifact)
    {
        if (Artifact.GetTypeSO() != ArtifactTypeSO)
            return;

        ResetArtifact();
    }

    private void ArtifactBubbleManager_OnArtifactEquip(Artifact Artifact)
    {
        if (artifact != null || Artifact.GetTypeSO() != ArtifactTypeSO)
            return;

        SetArtifact(Artifact);
    }

    private void SetArtifact(Artifact Artifact)
    {
        UnsubscribeEvents();
        artifact = Artifact;
        SubscribeEvents();
        InitVisual();
    }

    private void SubscribeEvents()
    {
        if (artifact == null)
            return;

        artifact.OnIEntityChanged += Artifact_OnIEntityChanged;
    }

    private void UnsubscribeEvents()
    {
        if (artifact == null)
            return;

        artifact.OnIEntityChanged -= Artifact_OnIEntityChanged;
    }

    private void Artifact_OnIEntityChanged(IEntity IEntity)
    {
        UpdateVisual();
    }

    private void ResetArtifact()
    {
        SetArtifact(null);
    }

    private void InitVisual()
    {
        UpdateVisual();
        ItemImage.gameObject.SetActive(artifact != null);
        PlaceholderImage.gameObject.SetActive(!ItemImage.gameObject.activeSelf);


        if (artifact == null)
        {
            ItemImage.overrideSprite = null;
            return;
        }

        ItemImage.overrideSprite = artifact.GetIcon();
    }

    private void UpdateVisual()
    {
        LevelTxt.gameObject.SetActive(artifact != null);

        if (artifact == null)
            return;

        LevelTxt.text = "+" + artifact.level;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        OnArtifactSphereSelect?.Invoke(this);
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        artifactBubbleManager.OnArtifactEquip -= ArtifactBubbleManager_OnArtifactEquip;
        artifactBubbleManager.OnArtifactUnEquip -= ArtifactBubbleManager_OnArtifactUnEquip;
        artifactBubbleManager.OnArtifactInventoryChanged -= ArtifactBubbleManager_OnArtifactInventoryChanged;
    }
}
