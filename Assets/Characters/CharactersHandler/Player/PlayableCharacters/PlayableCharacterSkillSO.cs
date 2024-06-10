using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterSkillSO", menuName = "ScriptableObjects/PlayerCharactersManager/PlayableCharacterSkillSO")]
public class PlayableCharacterSkillSO : ScriptableObject
{
    [field: SerializeField] public string SkillName { get; private set; }

    [field: SerializeField] public float SkillCooldown { get; private set; }
    [field: SerializeField] public Sprite SkillSprite { get; private set; }
}
