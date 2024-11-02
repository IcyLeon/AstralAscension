using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopoutMessage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MessageTxt;
    [SerializeField] private float FadeAnimationTime;
    [SerializeField] private CanvasGroup canvasGroup;
    private Coroutine fadeInandOutCoroutine;
    
    public void SetMessage(string Message)
    {
        MessageTxt.text = Message;
        gameObject.SetActive(true);
        UpdateFadeInAndOut();
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }


    private void UpdateFadeInAndOut()
    {
        if (fadeInandOutCoroutine != null)
        {
            StopCoroutine(fadeInandOutCoroutine);
        }

        fadeInandOutCoroutine = StartCoroutine(FadeandOutIEnumerator(true));
    }

    private IEnumerator FadeandOutIEnumerator(bool fadeIn)
    {
        float dt = 0;
        float targetAlpha = 0;

        if (fadeIn)
        {
            canvasGroup.alpha = 0;
            targetAlpha = 1;
        }

        while (dt < FadeAnimationTime)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, dt / FadeAnimationTime);
            dt += Time.unscaledDeltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (fadeIn)
        {
            yield return new WaitForSecondsRealtime(2.5f);
            fadeInandOutCoroutine = StartCoroutine(FadeandOutIEnumerator(false));
        }
        else
        {
            gameObject.SetActive(false);
        }

        fadeInandOutCoroutine = null;
    }
}
