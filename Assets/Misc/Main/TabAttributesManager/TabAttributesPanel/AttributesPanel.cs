using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesPanel : TabAttributesPanel
{ 
    [SerializeField] private Button OutfitButton;
    [SerializeField] private OutfitAttributesPanel outfitAttributesPanel;

    private void Awake()
    {
        OutfitButton.onClick.AddListener(OnOutfitButtonClick);
    }

    private void OnOutfitButtonClick()
    {
        outfitAttributesPanel.gameObject.SetActive(true);
    }
}
