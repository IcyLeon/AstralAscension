using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ModelsHandler : MonoBehaviour
{
    [Header("Default placeholder (for Editor only)")]
    private GameObject skinModel;
    [SerializeField] private GameObject editorPreviewModel;
    private SkinSO skinSO;

    private void Awake()
    {
        DestroyPreview();
    }

    public void SetSkinSO(SkinSO SkinSO)
    {
        if (skinSO == SkinSO)
            return;

        DestroySkin(ref skinModel);
        SpawnSkin(SkinSO);
        skinSO = SkinSO;
    }


    private void SpawnSkin(SkinSO SkinSO)
    {
        if (SkinSO == null)
            return;

        skinModel = SkinSO.LoadSkin(transform);
    }

    private void DestroyPreview()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif
        DestroySkin(ref editorPreviewModel);
    }

    private void DestroySkin(ref GameObject SkinModel)
    {
        if (!SkinModel)
            return;

        Destroy(SkinModel);
    }
}
