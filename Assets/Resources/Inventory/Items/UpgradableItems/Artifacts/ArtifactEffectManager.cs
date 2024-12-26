using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArtifactEffectManager
{
    private ArtifactInventory inventory;
    private EffectManager effectManager;

    public ArtifactEffectManager(ArtifactInventory ArtifactInventory, EffectManager EffectManager)
    {
        inventory = ArtifactInventory;
        effectManager = EffectManager;
        inventory.OnArtifactFamilyAdd += Inventory_OnArtifactFamilyAdd;
        inventory.OnArtifactFamilyRemove += Inventory_OnArtifactFamilyRemove;
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
        inventory.OnArtifactFamilyAdd -= Inventory_OnArtifactFamilyAdd;
        inventory.OnArtifactFamilyRemove -= Inventory_OnArtifactFamilyRemove;

        for (int i = inventory.artifactList.Count - 1; i >= 0; i--)
        {
            Inventory_OnArtifactFamilyRemove(inventory.artifactList.ElementAt(i).Value);
        }
    }

    public int GetBuffCurrentIndex(ArtifactFamilySO artifactFamilySO)
    {
        if (!inventory.artifactList.TryGetValue(artifactFamilySO, out ArtifactFamily artifactFamily))
            return -1;

        return artifactFamily.GetBuffCurrentIndex();
    }
}
