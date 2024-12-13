using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DamageableEntitySO : CharactersSO
{
    [field: SerializeField] public ElementsSO ElementSO { get; private set; }

    public virtual CharacterDataStat CreateCharacterDataStat()
    {
        return new CharacterDataStat(this);
    }
}
