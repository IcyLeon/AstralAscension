using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelsHandler : MonoBehaviour
{
    [Header("Default placeholder (for Editor only)")]
    [SerializeField] private GameObject editorPreviewModel;

    private void Awake()
    {
        DestroyPreview();
    }

    private void DestroyPreview()
    {
#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif
        if (editorPreviewModel)
        {
            Destroy(editorPreviewModel);
            editorPreviewModel = null;
        }
    }
}
