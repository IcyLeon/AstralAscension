using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Player : Healthbar
{
    [SerializeField] private ActiveCharacter activeCharacter;

    // Start is called before the first frame update
    private void Awake()
    {
        ActiveCharacter.OnPlayerCharacterExit += ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch += ActiveCharacter_OnPlayerCharacterSwitch;
    }

    private void ActiveCharacter_OnPlayerCharacterExit(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        if (damageable == null)
            return;

        damageable.OnTakeDamage -= Damageable_OnTakeDamage;
    }

    private void ActiveCharacter_OnPlayerCharacterSwitch(CharacterDataStat playerData, PlayableCharacters playableCharacters)
    {
        damageable = activeCharacter.GetPlayableCharacters(playerData);

        if (damageable != null)
        {
            damageable.OnTakeDamage += Damageable_OnTakeDamage;
            UpdateHealth();
        }

    }

    private void OnDestroy()
    {
        ActiveCharacter.OnPlayerCharacterExit -= ActiveCharacter_OnPlayerCharacterExit;
        ActiveCharacter.OnPlayerCharacterSwitch -= ActiveCharacter_OnPlayerCharacterSwitch;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
