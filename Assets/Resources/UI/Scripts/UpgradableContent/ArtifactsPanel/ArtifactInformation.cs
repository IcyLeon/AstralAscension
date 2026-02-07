using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactInformation : MonoBehaviour
{
    private ArtifactPanelContent artifactPanelContent;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    public event Action OnClose;

    private void Awake()
    {
        artifactPanelContent = GetComponentInChildren<ArtifactPanelContent>(true);
    }
    public ItemTypeTabGroup itemTypeTabGroup
    {
        get
        {
            return artifactPanelContent.itemTypeTabGroup;
        }
    }

    public void ClosePanel()
    {
        OnClose?.Invoke();
        MainPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        MainPanel.SetActive(true); // temp
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

    private void ArtifactPanelContent_OnItemQualitySelect(IData IData)
    {
        ItemContentDisplay.SetIItem(IData);
    }
}
