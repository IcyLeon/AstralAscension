using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AssetManager;

public class RarityStars : MonoBehaviour
{
    private ObjectPool<MonoBehaviour> starPool;
    private ItemManagerSO ItemAssetManagerSO;
    private ItemQuality itemQuality;

    private void Awake()
    {
        itemQuality = GetComponentInParent<ItemQuality>();
    }

    private void Start()
    {
        ItemAssetManagerSO = instance.ItemAssetManagerSO;
        SpawnStars();
    }

    private void SpawnStars()
    {
        if (starPool != null)
            return;

        starPool = new ObjectPool<MonoBehaviour>(ItemAssetManagerSO.StarPrefab, transform, (int)itemQuality.itemQualityDisplayData.iData.GetRaritySO().Rarity, true);
    }

}
