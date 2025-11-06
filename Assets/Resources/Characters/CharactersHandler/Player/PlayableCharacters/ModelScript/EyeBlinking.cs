using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    private SkinnedMeshRenderer SkinnedMeshRenderer;
    [SerializeField] private string[] BlendShapeName;
    private List<int> eyeblinkingidxList;
    private Coroutine EyeBlinkCoroutine;
    private WaitForSeconds TimeToBlink = new WaitForSeconds(3.5f);

    private void Awake()
    {
        SkinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        eyeblinkingidxList = GetMeshIdxByName(BlendShapeName);
    }
    private void OnEnable()
    {
        if (EyeBlinkCoroutine == null)
            EyeBlinkCoroutine = StartCoroutine(BlinkCoroutine());

    }

    private List<int> GetMeshIdxByName(string[] nameBlendShapeList)
    {
        List<int> Indexlist = new();

        if (SkinnedMeshRenderer == null)
            return Indexlist;

        foreach (string name in nameBlendShapeList)
        {
            int index = SkinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(name);
            if (index >= 0)
            {
                Indexlist.Add(index);
            }
        }
        return Indexlist;
    }

    private void OnDisable()
    {
        if (EyeBlinkCoroutine != null)
        {
            StopCoroutine(EyeBlinkCoroutine);
            EyeBlinkCoroutine = null;
        }    
    }

    private IEnumerator BlinkCoroutine()
    {
        yield return TimeToBlink;

        float elapsedTime = 0f;
        float duration = 0.2f;
        float maxValue = 100f;

        while (elapsedTime <= duration)
        {
            foreach(var idx in eyeblinkingidxList)
            {
                SkinnedMeshRenderer.SetBlendShapeWeight(idx, Mathf.Sin(Mathf.PI * (elapsedTime / duration)) * maxValue);
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var idx in eyeblinkingidxList)
        {
            SkinnedMeshRenderer.SetBlendShapeWeight(idx, 0f);
        }

        StartCoroutine(BlinkCoroutine());
    }
}
