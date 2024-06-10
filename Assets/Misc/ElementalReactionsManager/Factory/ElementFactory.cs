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

public static class ElementFactoryManager
{
    private static Dictionary<ElementEnums, System.Func<ElementFactory>> Element_Dict = new()
    {
        { ElementEnums.PYRO, () => new PyroFactory() },
        { ElementEnums.CRYO, () => new CryoFactory() },
        { ElementEnums.HYDRO, () => new HydroFactory() },
        { ElementEnums.ELECTRO, () => new ElectroFactory() },
    };

    public static ElementFactory CreateElementFactory(ElementEnums e)
    {
        if (!Element_Dict.ContainsKey(e))
            return null;

        return Element_Dict[e]();
    }
}


public abstract class ElementFactory
{
    public abstract Elements CreateElements(ElementsSO elementsSO, CharacterDataStat c);
}

public class PyroFactory : ElementFactory
{
    public override Elements CreateElements(ElementsSO elementsSO, CharacterDataStat c)
    {
        return new Pyro(elementsSO, c);
    }
}

public class HydroFactory : ElementFactory
{
    public override Elements CreateElements(ElementsSO elementsSO, CharacterDataStat c)
    {
        return new Hydro(elementsSO, c);
    }
}

public class ElectroFactory : ElementFactory
{
    public override Elements CreateElements(ElementsSO elementsSO, CharacterDataStat c)
    {
        return new Electro(elementsSO, c);
    }
}

public class CryoFactory : ElementFactory
{
    public override Elements CreateElements(ElementsSO elementsSO, CharacterDataStat c)
    {
        return new Cryo(elementsSO, c);
    }
}