using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArtifactEffectManager
{
    private ArtifactEquipment artifactEquipment;
    private EffectManager effectManager;

    public ArtifactEffectManager(ArtifactEquipment ArtifactEquipment, EffectManager EffectManager)
    {
        artifactEquipment = ArtifactEquipment;
        effectManager = EffectManager;
        artifactEquipment.OnArtifactFamilyAdd += Inventory_OnArtifactFamilyAdd;
        artifactEquipment.OnArtifactFamilyRemove += Inventory_OnArtifactFamilyRemove;
    }

    private void Inventory_OnArtifactFamilyRemove(ArtifactFamily ArtifactFamily)
    {
        ArtifactFamily.OnArtifactBuffAdd -= ArtifactFamily_OnArtifactBuffAdd;
        ArtifactFamily.OnArtifactBuffRemove -= ArtifactFamily_OnArtifactBuffRemove;
    }

    private void Inventory_OnArtifactFamilyAdd(ArtifactFamily ArtifactFamily)
    {
        ArtifactFamily.OnArtifactBuffAdd += ArtifactFamily_OnArtifactBuffAdd;
        ArtifactFamily.OnArtifactBuffRemove += ArtifactFamily_OnArtifactBuffRemove;
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
        artifactEquipment.OnArtifactFamilyAdd -= Inventory_OnArtifactFamilyAdd;
        artifactEquipment.OnArtifactFamilyRemove -= Inventory_OnArtifactFamilyRemove;

        for (int i = artifactEquipment.artifactFamilyList.Count - 1; i >= 0; i--)
        {
            Inventory_OnArtifactFamilyRemove(artifactEquipment.artifactFamilyList.ElementAt(i).Value);
        }
    }

    public int GetBuffCurrentIndex(ArtifactFamilySO artifactFamilySO)
    {
        if (!artifactEquipment.artifactFamilyList.TryGetValue(artifactFamilySO, out ArtifactFamily artifactFamily))
            return -1;

        return artifactFamily.GetBuffCurrentIndex();
    }
}
