using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharacterProfileSO", menuName = "ScriptableObjects/PlayerCharactersManager/PlayerCharacterProfileSO")]
public class PlayerCharacterProfileSO : CharacterProfileSO
{
    [field: SerializeField] public string VoiceActor { get; private set; }
    [field: SerializeField] public PlayableCharacterVoicelinesSO PlayableCharacterVoicelinesSO { get; private set; }
    [field: SerializeField, Header("Skins")] public SkinSO[] SkinSOList { get; private set; }

}
