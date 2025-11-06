using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSO : ScriptableObject
{
    [field: SerializeField] public string SkillName { get; private set; }

    [field: SerializeField] public float SkillCooldown { get; private set; }
    [field: SerializeField] public Sprite SkillSprite { get; private set; }
}
