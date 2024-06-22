using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider healthBarSlider;
    private CharacterDataStat characterDataStat;

    protected void UpdateHealth()
    {
        if (characterDataStat == null)
            return;

        healthBarSlider.value = characterDataStat.GetCurrentHealth() / characterDataStat.GetMaxHealth();
    }

    public void SetCharacterDataStat(CharacterDataStat c)
    {
        characterDataStat = c;
    }
    public CharacterDataStat GetCharacterDataStat()
    {
        return characterDataStat;
    }


    private void Update()
    {
        UpdateHealth();
    }
}
