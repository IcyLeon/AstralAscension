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
    private const float spawnOffsetDistance = 0.25f;
    private Vector2 offsetPosition;
    private Vector2 vel;
    private RectTransform rt;

    private Camera mainCamera;

    private void Awake()
    {
        originlocalScale = Vector3.one * 3.5f;
        vel = Vector2.up * 20f;
        rt = GetComponent<RectTransform>();
        canvasGroup.alpha = 0f;
    }
    private void Start()
    {
        mainCamera = GetMainCamera();
        ResetOffset();
    }

    private void OnEnable()
    {
        StartCoroutine(ScaleAnimation());
        StartCoroutine(FadeAnimation(true));
    }

    private void OnDisable()
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

    public void SetDamageTextValue(string txt, Vector3 WorldPosition, Color color)
    {
        ValueText.text = txt;
        ValueText.color = color;
        UpdatePosition(WorldPosition + new Vector3(GetRandomPositionOffset(), GetRandomPositionOffset(), GetRandomPositionOffset()));
    }

    private float GetRandomPositionOffset()
    {
        return Random.Range(-spawnOffsetDistance, spawnOffsetDistance);
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
        if (mainCamera == null)
            return;

        Vector2 pos = mainCamera.WorldToScreenPoint(originPosition);
        offsetPosition += vel * Time.deltaTime; 
        rt.anchoredPosition = pos + offsetPosition;
    }

    private Camera GetMainCamera()
    {
        CameraManager cameraManager = CameraManager.instance;

        MainCamera mainCamera = cameraManager.GetMainCamera(gameObject.scene);

        if (mainCamera == null)
            return null;

        return mainCamera.Camera;
    }
}
