using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactInformation : MonoBehaviour
{
    private ArtifactPanelContent artifactPanelContent;
    //public ItemTypeTabGroup itemTypeTabGroup { get; private set; }
    [SerializeField] private ItemContentDisplay ItemContentDisplay;

    private void Awake()
    {
        artifactPanelContent = GetComponentInChildren<ArtifactPanelContent>(true);
    }

    //private void Start()
    //{
    //    itemTypeTabGroup = artifactPanelContent.ItemTypeTabGroup;
    //}

    public ItemTypeTabGroup itemTypeTabGroup
    {
        get
        {
            return artifactPanelContent.ItemTypeTabGroup;
        }
    }

    public void OpenPanel()
    {
        gameObject.SetActive(true); // temp
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnDisable()
    {
        UnsubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        artifactPanelContent.OnItemQualitySelect += ArtifactPanelContent_OnItemQualitySelect;
    }

    private void UnsubscribeEvents()
    {
        artifactPanelContent.OnItemQualitySelect -= ArtifactPanelContent_OnItemQualitySelect;
    }

    private void ArtifactPanelContent_OnItemQualitySelect(ItemQualityButton ItemQualityButton)
    {
        ItemContentDisplay.SetIItem(ItemQualityButton.ItemQuality.iItem);
    }
}
