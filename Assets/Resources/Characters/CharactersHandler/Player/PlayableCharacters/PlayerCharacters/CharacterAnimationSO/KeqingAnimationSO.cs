using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeqingAnimationSO", menuName = "ScriptableObjects/PlayableCharacterAnimation/KeqingAnimationSO")]
public class KeqingAnimationSO : PlayableCharacterAnimationSO
{
    [field: SerializeField] public string aimParameter { get; private set; }
    [field: SerializeField] public string throwParameter { get; private set; }
    [field: SerializeField] public string elementalSkillSlashParameter { get; private set; }
}
