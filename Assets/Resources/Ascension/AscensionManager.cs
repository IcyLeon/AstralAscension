using System;
using UnityEngine;

public class AscensionManager
{
    private AscensionInfomationFactory ascensionInfomationFactory;
    private PlayableCharacterDataStat playableCharacterDataStat;
    private AscensionInformation currentAscensionNode;
    public int ascensionLvl { get; private set; }
    public event Action<Ascension> OnAscensionChanged;

    public AscensionManager(AscensionSO ascensionSO)
    {
        ascensionInfomationFactory = new AscensionInfomationFactory(ascensionSO);
        ascensionLvl = 1;
        currentAscensionNode = GetNextAscensionNode(currentAscensionNode);
    }

    public void SetPlayableCharacterDataStat(PlayableCharacterDataStat PlayableCharacterDataStat)
    {
        playableCharacterDataStat = PlayableCharacterDataStat;
    }

    private AscensionInformation GetNextAscensionNode(AscensionInformation currentNode)
    {
        return ascensionInfomationFactory.GetNextAscensionNode(currentNode);
    }


    public void Ascend()
    {
        AscensionInformation nextAscension = GetNextAscensionNode(currentAscensionNode);

        if (nextAscension == null)
            return;

        currentAscensionNode = nextAscension;
        ascensionLvl++;
        OnAscensionChanged?.Invoke(currentAscensionNode.ascension);
    }
}
