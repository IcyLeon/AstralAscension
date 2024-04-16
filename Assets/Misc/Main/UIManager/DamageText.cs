using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ValueText;
    [SerializeField] private CanvasGroup canvasGroup;
    private Vector3 originPosition;
    private Vector3 originlocalScale;
    private float spawnOffsetPosition;
    private Vector2 offsetPosition;
    private Vector2 vel;
    private RectTransform rt;

    private void Awake()
    {
        originlocalScale = Vector3.one * 2.8f;
        spawnOffsetPosition = 0.35f;
        vel = Vector2.up * 15f;
        rt = GetComponent<RectTransform>();
        canvasGroup.alpha = 0f;
    }
    private void Start()
    {
        ResetOffset();
    }

    private void OnEnable()
    {
        ResetOffset();
    }

    private void ResetOffset()
    {
        offsetPosition = Vector2.zero;
        transform.localScale = originlocalScale;
    }
    private void UpdatePosition(Vector3 WorldPosition)
    {
        originPosition = WorldPosition;
    }

    public void SetDamageValue(float Damage, Vector3 WorldPosition)
    {
        ValueText.text = Damage.ToString();
        UpdatePosition(WorldPosition + new Vector3(GetRandomPositionOffset(), GetRandomPositionOffset(), GetRandomPositionOffset()));
        gameObject.SetActive(true);
        StartCoroutine(ScaleAnimation());
        StartCoroutine(FadeAnimation(true));
    }

    private float GetRandomPositionOffset()
    {
        return Random.Range(-spawnOffsetPosition, spawnOffsetPosition);
    }

    private IEnumerator ScaleAnimation()
    {
        float ElaspedTime = 0f;
        float AnimationTime = 0.35f;
        Vector3 TargetScale = Vector3.one * Random.Range(0.85f, 1.15f);

        while (ElaspedTime <= AnimationTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, TargetScale, ElaspedTime / AnimationTime);
            ElaspedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeAnimation(bool fadeIn)
    {
        float ElaspedTime = 0f;
        float AnimationTime = 0.25f;
        float TargetAlpha = 0f;

        if (fadeIn)
            TargetAlpha = 1f;

        while (ElaspedTime <= AnimationTime)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, TargetAlpha, ElaspedTime / AnimationTime);
            ElaspedTime += Time.deltaTime;
            yield return null;
        }

        if (fadeIn)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FadeAnimation(false));
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(originPosition);
        offsetPosition += vel * Time.deltaTime; 
        rt.anchoredPosition = pos + offsetPosition;
    }
}
