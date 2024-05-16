using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIconDisplay : MonoBehaviour
{
    private ElementsSO elementsSO;
    private ElementUI ElementUIRef;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Material materialInstance;
    [SerializeField] private Image ElementImage;
    private Coroutine ToggleCoroutine;

    private void OnEnable()
    {
        FadeIn();
    }

    private void OnDisable()
    {
        ToggleCoroutine = null;

        if (elementsSO != null)
            ElementUIRef.EID_Dict.Remove(elementsSO);

        canvasGroup.alpha = 0f;
        elementsSO = null;
    }

    public void SetElementUI(ElementUI e)
    {
        ElementUIRef = e;
    }

    public void SetElementsSO(ElementsSO e)
    {
        elementsSO = e;

        if (elementsSO != null)
        {
            ElementImage.sprite = elementsSO.ElementSprite;
            ElementImage.material.SetColor("_GlowColor", elementsSO.GetGlowIconColor());
        }

        gameObject.SetActive(true);
    }

    public void FadeIn()
    {
        if (ToggleCoroutine != null)
        {
            StopCoroutine(ToggleCoroutine);
        }

        ToggleCoroutine = StartCoroutine(ToggleAnimation(true));
    }

    public void FadeOut()
    {
        ToggleCoroutine = StartCoroutine(ToggleAnimation(false));
    }

    private IEnumerator ToggleAnimation(bool active)
    {
        float animationTime = 0.35f;
        float ElapsedTime = 0f;
        float targetAlpha = 1f;

        if (!active)
        {
            targetAlpha = 0f;
            yield return new WaitForSeconds(animationTime);
        }

        while (ElapsedTime < animationTime)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, ElapsedTime / animationTime);
            ElapsedTime += Time.deltaTime;
            yield return null;
        }

        gameObject.SetActive(active);
    }

    // Start is called before the first frame update
    void Start()
    {
        ElementImage.material = Instantiate(materialInstance);
    }
}
