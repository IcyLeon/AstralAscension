using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalReactionsManager;

public enum ElementEnums
{
    PYRO,
    CRYO,
    HYDRO,
    DENDRO,
    ELECTRO,
    ANEMO
}

public abstract class ElementFactory
{
    public abstract Elements CreateElements(CharacterDataStat c);
}

public class PyroFactory : ElementFactory
{
    public override Elements CreateElements(CharacterDataStat c)
    {
        return null;
    }
}