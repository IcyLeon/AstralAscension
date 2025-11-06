using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfileSO", menuName = "ScriptableObjects/PlayerCharactersManager/CharacterProfileSO")]
public class CharacterProfileSO : ScriptableObject
{
    [field: SerializeField, Header("Base Character Infomation")] public string Name { get; private set; }
    [field: SerializeField, TextArea] public string Description { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public ItemRaritySO RaritySO { get; private set; }
}
