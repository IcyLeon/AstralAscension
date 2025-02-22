using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterProfileSO", menuName = "ScriptableObjects/PlayerCharactersManager/PlayerCharacterProfileSO")]
public class PlayerCharacterProfileSO : CharacterProfileSO
{
    [field: SerializeField] public string VoiceActor { get; private set; }
}
