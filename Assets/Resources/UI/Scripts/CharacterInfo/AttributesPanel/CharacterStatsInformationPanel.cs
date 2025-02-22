using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatsInformationPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CharacterNameTxt;
    [SerializeField] private TextMeshProUGUI CharacterDescTxt;
    private CharacterDataStat currentCharacterDataStat;
    private CharacterScreenPanel characterScreenPanel;

    private void Awake()
    {
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnIconSelected += CharacterScreenPanel_OnIconSelected;
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        currentCharacterDataStat = characterScreenPanel.currentCharacterSelected;

        if (currentCharacterDataStat == null)
            return;

        CharacterNameTxt.text = currentCharacterDataStat.damageableEntitySO.GetName();
        CharacterDescTxt.text = currentCharacterDataStat.damageableEntitySO.GetDescription();
    }

    private void CharacterScreenPanel_OnIconSelected()
    {
        UpdateVisual();
    }

    private void OnDestroy()
    {
        characterScreenPanel.OnIconSelected -= CharacterScreenPanel_OnIconSelected;
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
