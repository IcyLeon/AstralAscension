using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactSelectionContent : MonoBehaviour
{
    [SerializeField] private ArtifactPanelContent ArtifactPanelContent;
    [SerializeField] private ItemContentDisplay ItemContentDisplay;

    // Start is called before the first frame update
    private void Awake()
    {
        ArtifactPanelContent.OnItemQualityButtonChanged += ArtifactPanelContent_OnItemQualityButtonChanged;
    }

    private void ArtifactPanelContent_OnItemQualityButtonChanged(object sender, ItemQualityButtonEvent e)
    {
        ItemContentDisplay.SetInterfaceItem(e.selectedItemQualityButton.ItemQuality.iItem);
    }

    private void OnDestroy()
    {
        ArtifactPanelContent.OnItemQualityButtonChanged -= ArtifactPanelContent_OnItemQualityButtonChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
