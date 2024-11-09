using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEffectManager
{
    private EffectManager effectManager;
    private CharacterDataStat characterDataStat;

    public ArtifactEffectManager(CharacterDataStat CharacterDataStat, EffectManager EffectManager)
    {
        characterDataStat = CharacterDataStat;
        effectManager = EffectManager;
        characterDataStat.OnItemRemove += PlayableCharacterDataStat_OnItemRemove;
        characterDataStat.OnItemAdd += PlayableCharacterDataStat_OnItemAdd;
    }

    private int CountArtifactPiece(ArtifactSO ArtifactSO)
    {
        int count = 0;

        foreach (var artifactKeyPair in characterDataStat.equippeditemList)
        {
            ArtifactSO artifactSO = artifactKeyPair.Value.GetIItem() as ArtifactSO;

            if (artifactSO != null && artifactSO.IsSameFamily(ArtifactSO))
            {
                count++;
            }
        }

        return count;
    }

    private void PlayableCharacterDataStat_OnItemAdd(IItem IItem)
    {
        if (!IsWithinPieceEvent(IItem))
            return;

        ArtifactSO artifactSO = IItem.GetIItem() as ArtifactSO;

        if (artifactSO == null)
            return;

        int eventCount = GetPieceEventCount(IItem);

        ArtifactEffectPieceFactory artifactEffectPieceFactory = artifactSO.ArtifactFamilySO.CreateArtifactEffectPieceFactory();

        ArtifactEffect ArtifactEffect = artifactEffectPieceFactory.CreatePieceEffect(eventCount - 1);

        effectManager.AddEffect(ArtifactEffect);
    }

    private void PlayableCharacterDataStat_OnItemRemove(IItem IItem)
    {
        if (IsWithinPieceEvent(IItem))
            return;

        ArtifactSO artifactSO = IItem.GetIItem() as ArtifactSO;

        if (artifactSO == null)
            return;

        int eventCount = GetPieceEventCount(IItem);

        ArtifactEffectPieceFactory artifactEffectPieceFactory = artifactSO.ArtifactFamilySO.CreateArtifactEffectPieceFactory();

        ArtifactEffect ArtifactEffectInfo = artifactEffectPieceFactory.GetArtifactEffectInformation(eventCount);

        BuffEffect ExistBuffEffect = effectManager.GetBuffTypeAlreadyExist(ArtifactEffectInfo);

        effectManager.RemoveEffect(ExistBuffEffect);
    }

    private int GetTotalPiece(IItem IItem)
    {
        ArtifactSO artifactSO = IItem.GetIItem() as ArtifactSO;

        if (artifactSO == null)
            return 0;

        return CountArtifactPiece(artifactSO);
    }

    private bool IsWithinPieceEvent(IItem IItem)
    {
        return (GetTotalPiece(IItem) % ArtifactManager.PIECE_COUNT_EVENT) == 0;
    }

    public int GetPieceEventCount(IItem IItem)
    {
        return (GetTotalPiece(IItem) / ArtifactManager.PIECE_COUNT_EVENT);
    }

    public void OnDestroy()
    {
        characterDataStat.OnItemRemove -= PlayableCharacterDataStat_OnItemRemove;
        characterDataStat.OnItemAdd -= PlayableCharacterDataStat_OnItemAdd;
    }
}
