using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    [SerializeField] private Characters characters;
    [SerializeField] private string EyeBlinkHash;
    private Coroutine EyeBlinkCoroutine;
    private WaitForSeconds TimeToBlink = new WaitForSeconds(3.5f);
    private void OnEnable()
    {
        if (EyeBlinkCoroutine == null)
            EyeBlinkCoroutine = StartCoroutine(BlinkCoroutine());
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
        while (true)
        {
            yield return TimeToBlink;
            characters.Animator.SetTrigger(EyeBlinkHash);
        }
    }
}
