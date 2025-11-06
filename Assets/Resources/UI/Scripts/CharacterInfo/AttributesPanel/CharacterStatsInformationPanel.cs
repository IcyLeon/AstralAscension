using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatsInformationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CharacterNameTxt;
    [SerializeField] private TextMeshProUGUI CharacterDescTxt;
    private CharacterEquipmentManager characterEquipmentManager;
    private CharacterScreenPanel characterScreenPanel;

    private void Awake()
    {
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnCharacterIconSelected += CharacterScreenPanel_OnIconSelected;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        characterEquipmentManager = characterScreenPanel.characterEquipmentManager;

        if (characterEquipmentManager == null)
            return;

        CharacterNameTxt.text = characterEquipmentManager.charactersSO.GetName();
        CharacterDescTxt.text = characterEquipmentManager.charactersSO.GetDescription();
    }

    private void CharacterScreenPanel_OnIconSelected()
    {
        UpdateVisual();
    }

    private void OnDestroy()
    {
        characterScreenPanel.OnCharacterIconSelected -= CharacterScreenPanel_OnIconSelected;
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
