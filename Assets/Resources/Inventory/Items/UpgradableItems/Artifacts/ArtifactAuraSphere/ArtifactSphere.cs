using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArtifactSphere : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private ItemTypeSO ItemTypeSO;
    [SerializeField] private Image PlaceholderImage;
    [SerializeField] private Image ItemImage;
    public event Action<ArtifactSphere> OnArtifactSphereSelect;
    private ArtifactSphereManager artifactSphereManager;
    private Artifact artifact;

    private void Awake()
    {
        artifactSphereManager = GetComponentInParent<ArtifactSphereManager>();
        artifactSphereManager.OnArtifactInventoryChanged += ArtifactSphereManager_OnArtifactInventoryChanged;
        artifactSphereManager.OnArtifactEquip += ArtifactSphereManager_OnArtifactEquip;
        artifactSphereManager.OnArtifactUnEquip += ArtifactSphereManager_OnArtifactUnEquip;
    }

    private void ArtifactSphereManager_OnArtifactInventoryChanged()
    {
        ResetArtifact();
    }

    private void ArtifactSphereManager_OnArtifactUnEquip(Artifact Artifact)
    {
        if (Artifact.GetIItem().GetTypeSO() != ItemTypeSO)
            return;

        ResetArtifact();
    }

    private void ArtifactSphereManager_OnArtifactEquip(Artifact Artifact)
    {
        if (artifact != null || Artifact.GetIItem().GetTypeSO() != ItemTypeSO)
            return;

        SetArtifact(Artifact);
    }

    private void Start()
    {
        ResetArtifact();
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

        ItemImage.overrideSprite = artifact.GetIItem().GetIcon();
    }

    private void UpdateVisual()
    {
        LevelTxt.gameObject.SetActive(artifact != null);

        if (artifact == null)
            return;

        LevelTxt.text = "+" + artifact.GetLevel();
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
        artifactSphereManager.OnArtifactEquip -= ArtifactSphereManager_OnArtifactEquip;
        artifactSphereManager.OnArtifactUnEquip -= ArtifactSphereManager_OnArtifactUnEquip;
        artifactSphereManager.OnArtifactInventoryChanged -= ArtifactSphereManager_OnArtifactInventoryChanged;
    }
}
