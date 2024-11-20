using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterArtifactManager
{
    private CharacterDataStat characterDataStat;
    private Dictionary<ArtifactFamilySO, Dictionary<ItemTypeSO, Artifact>> artifactList;
    private ArtifactEffectManager artifactEffectManager;

    public event Action<Artifact> OnArtifactAdd;
    public event Action<Artifact> OnArtifactRemove;

    public CharacterArtifactManager(CharacterDataStat CharacterDataStat, EffectManager effectManager)
    {
        characterDataStat = CharacterDataStat;
        artifactEffectManager = new(this, effectManager);
        artifactList = new();

        characterDataStat.OnItemAdd += CharacterDataStat_OnItemAdd;
        characterDataStat.OnItemRemove += CharacterDataStat_OnItemRemove;
    }

    public int GetPieceEventCount(IItem iItem)
    {
        Artifact artifact = iItem as Artifact;

        if (artifact == null)
            return -1;

        return artifactEffectManager.GetPieceEventCount(artifact);
    }

    public int GetTotalPiece(ArtifactSO artifactSO)
    {
        if (!artifactList.ContainsKey(artifactSO.ArtifactFamilySO))
            return 0;

        return artifactList[artifactSO.ArtifactFamilySO].Count;
    }

    private void CharacterDataStat_OnItemRemove(IItem IItem)
    {
        Artifact artifact = IItem as Artifact;

        if (artifact == null)
            return;

        Dictionary<ItemTypeSO, Artifact> artifactDictionary = artifactList[artifact.artifactSO.ArtifactFamilySO];
        artifactDictionary.Remove(artifact.GetTypeSO());

        if (artifactDictionary.Count == 0)
        {
            artifactList.Remove(artifact.artifactSO.ArtifactFamilySO);
        }

        OnArtifactRemove?.Invoke(artifact);
        artifact.CallOnItemChanged();
    }

    private void CharacterDataStat_OnItemAdd(IItem IItem)
    {
        Artifact artifact = IItem as Artifact;

        if (artifact == null)
            return;

        ArtifactFamilySO artifactFamilySO = artifact.artifactSO.ArtifactFamilySO;

        if (!artifactList.ContainsKey(artifactFamilySO))
        {
            artifactList.Add(artifactFamilySO, new());
        }

        artifactList[artifactFamilySO].Add(artifact.GetTypeSO(), artifact);

        OnArtifactAdd?.Invoke(artifact);
        artifact.CallOnItemChanged();
    }

    public void OnDestroy()
    {
        artifactEffectManager.OnDestroy();
        characterDataStat.OnItemRemove -= CharacterDataStat_OnItemRemove;
        characterDataStat.OnItemAdd -= CharacterDataStat_OnItemAdd;
    }
}
