using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class DamageableEntitySO : CharactersSO
{
    [field: SerializeField] public ElementsSO ElementSO { get; private set; }

    public virtual CharacterDataStat CreateCharacterDataStat()
    {
        return new CharacterDataStat(this);
    }

    public DamageableCharacters CreateCharacter(Transform transform)
    {
        DamageableCharacters prefab = Prefab.GetComponent<DamageableCharacters>();

        if (prefab == null)
        {
            Debug.Log("CharacterPrefab does not have the component of DamageableCharacters");
            return null;
        }

        DamageableCharacters DamageableCharacters = Instantiate(prefab.gameObject, transform).GetComponent<DamageableCharacters>();
        DamageableCharacters.InitStateMachine();
        return DamageableCharacters;
    }
}
