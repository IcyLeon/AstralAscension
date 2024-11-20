using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class ArtifactEffectManager
{
    private EffectManager effectManager;
    private CharacterArtifactManager characterArtifactManager;

    public ArtifactEffectManager(CharacterArtifactManager CharacterArtifactManager, EffectManager EffectManager)
    {
        characterArtifactManager = CharacterArtifactManager;
        effectManager = EffectManager;
        characterArtifactManager.OnArtifactAdd += CharacterArtifactManager_OnArtifactAdd;
        characterArtifactManager.OnArtifactRemove += CharacterArtifactManager_OnArtifactRemove;
    }

    private void CharacterArtifactManager_OnArtifactRemove(Artifact artifact)
    {
        if (IsWithinPieceEvent(artifact))
            return;

        int eventCount = GetPieceEventCount(artifact);

        ArtifactEffectFactoryManager factoryManager = ArtifactManager.instance.ArtifactEffectFactories[artifact.artifactSO.ArtifactFamilySO];

        ArtifactEffect ArtifactEffectInfo = factoryManager.GetArtifactEffectInformation(eventCount).CreateArtifactEffect();

        BuffEffect ExistBuffEffect = effectManager.GetBuffTypeAlreadyExist(ArtifactEffectInfo);

        effectManager.RemoveEffect(ExistBuffEffect);
    }

    private void CharacterArtifactManager_OnArtifactAdd(Artifact artifact)
    {
        if (!IsWithinPieceEvent(artifact))
            return;

        int eventCount = GetPieceEventCount(artifact);

        ArtifactEffectFactoryManager factoryManager = ArtifactManager.instance.ArtifactEffectFactories[artifact.artifactSO.ArtifactFamilySO];

        ArtifactEffect ArtifactEffect = factoryManager.CreatePieceEffect(eventCount - 1);

        effectManager.AddEffect(ArtifactEffect);
    }

    private bool IsWithinPieceEvent(Artifact artifact)
    {
        return (characterArtifactManager.GetTotalPiece(artifact.artifactSO) % ArtifactManager.PIECE_COUNT) == 0;
    }

    public int GetPieceEventCount(Artifact artifact)
    {
        return characterArtifactManager.GetTotalPiece(artifact.artifactSO) / ArtifactManager.PIECE_COUNT;
    }

    public void OnDestroy()
    {
        characterArtifactManager.OnArtifactAdd -= CharacterArtifactManager_OnArtifactAdd;
        characterArtifactManager.OnArtifactRemove -= CharacterArtifactManager_OnArtifactRemove;
    }
}
