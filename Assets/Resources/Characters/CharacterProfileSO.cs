using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfileSO", menuName = "ScriptableObjects/PlayerCharactersManager/CharacterProfileSO")]
public class CharacterProfileSO : ScriptableObject
{
    [field: SerializeField, Header("Base Character Infomation")] public string CharacterName { get; private set; }
    [field: SerializeField, TextArea] public string CharacterDescription { get; private set; }
    [field: SerializeField] public Sprite CharacterIcon { get; private set; }
}
