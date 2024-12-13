using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArtifactEffectManager
{
    private CharacterDataStat characterDataStat;
    private Dictionary<ArtifactFamilySO, ArtifactFamily> artifactList;
    private EffectManager effectManager;

    public ArtifactEffectManager(CharacterDataStat CharacterDataStat, EffectManager EffectManager)
    {
        characterDataStat = CharacterDataStat;
        effectManager = EffectManager;
        artifactList = new();

        characterDataStat.OnEquip += CharacterDataStat_OnEquip;
        characterDataStat.OnUnEquip += CharacterDataStat_OnUnEquip;
    }

    private void CharacterDataStat_OnUnEquip(UpgradableItems UpgradableItems)
    {
        Artifact artifact = UpgradableItems as Artifact;

        if (artifact == null)
            return;

        Debug.Log("Unequipped");
        artifactList[artifact.artifactSO.ArtifactFamilySO].Remove(artifact);
    }

    private void ArtifactFamily_OnFamilyRemove(ArtifactFamily ArtifactFamily)
    {
        ArtifactFamily.OnArtifactBuffAdd -= ArtifactFamily_OnArtifactBuffAdd;
        ArtifactFamily.OnArtifactBuffRemove -= ArtifactFamily_OnArtifactBuffRemove;
        ArtifactFamily.OnFamilyRemove -= ArtifactFamily_OnFamilyRemove;
        artifactList.Remove(ArtifactFamily.artifactFamilySO);
    }


    private void CharacterDataStat_OnEquip(UpgradableItems UpgradableItems)
    {
        Artifact artifact = UpgradableItems as Artifact;

        if (artifact == null)
            return;

        ArtifactFamilySO artifactFamilySO = artifact.artifactSO.ArtifactFamilySO;

        if (!artifactList.ContainsKey(artifactFamilySO))
        {
            ArtifactFamily ArtifactFamily = artifactFamilySO.CreateArtifactFamily();
            ArtifactFamily.OnArtifactBuffAdd += ArtifactFamily_OnArtifactBuffAdd;
            ArtifactFamily.OnArtifactBuffRemove += ArtifactFamily_OnArtifactBuffRemove;
            ArtifactFamily.OnFamilyRemove += ArtifactFamily_OnFamilyRemove;
            artifactList.Add(artifactFamilySO, ArtifactFamily);
        }

        Debug.Log("Equipped");
        artifactList[artifactFamilySO].Add(artifact);
    }

    private void ArtifactFamily_OnArtifactBuffRemove(ArtifactBuffInformation ArtifactBuffInformation)
    {
        Debug.Log("Remove");
        ArtifactEffect ArtifactEffectInfo = ArtifactBuffInformation.buffPieceInfo.CreateArtifactEffect();
        BuffEffect ExistBuffEffect = effectManager.GetBuffTypeAlreadyExist(ArtifactEffectInfo);
        effectManager.RemoveEffect(ExistBuffEffect);
    }

    private void ArtifactFamily_OnArtifactBuffAdd(ArtifactBuffInformation ArtifactBuffInformation)
    {
        Debug.Log("Add");
        ArtifactEffect ArtifactEffectInfo = ArtifactBuffInformation.buffPieceInfo.CreateArtifactEffect();
        effectManager.AddEffect(ArtifactEffectInfo);
    }

    public void OnDestroy()
    {
        characterDataStat.OnEquip -= CharacterDataStat_OnEquip;
        characterDataStat.OnUnEquip -= CharacterDataStat_OnUnEquip;

        for(int i = artifactList.Count - 1; i >= 0; i--)
        {
            ArtifactFamily_OnFamilyRemove(artifactList.ElementAt(i).Value);
        }
    }

    public int GetBuffCurrentIndex(ArtifactFamilySO artifactFamilySO)
    {
        if (!artifactList.TryGetValue(artifactFamilySO, out ArtifactFamily artifactFamily))
            return -1;

        return artifactFamily.GetBuffCurrentIndex();
    }
}
