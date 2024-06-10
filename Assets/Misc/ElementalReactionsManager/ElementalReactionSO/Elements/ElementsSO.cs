using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "ScriptableObjects/ElementManager/ElementSO")]
public class ElementsSO : ElementsInfoSO
{
    [field: SerializeField] public ElementEnums Elements { get; private set; }
    [field: SerializeField] public Sprite ElementSprite { get; private set; }

    [ColorUsage(true, true)]
    [SerializeField] private Color GlowIconColor;

    public Color GetGlowIconColor()
    {
        return GlowIconColor;
    }

    public Elements CreateElements(CharacterDataStat target)
    {
        ElementFactory factory = ElementFactoryManager.CreateElementFactory(Elements);

        if (factory == null)
            return null;

        return factory.CreateElements(this, target);
    }
}
