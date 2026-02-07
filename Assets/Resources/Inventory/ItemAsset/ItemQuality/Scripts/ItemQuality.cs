using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static AssetManager;
using DG.Tweening;

[DisallowMultipleComponent]
public class ItemQuality : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemManagerSO ItemAssetManagerSO;
    [SerializeField] private TextMeshProUGUI DisplayText;
    [Range(8, 15)]
    [SerializeField] private int LimitTextAmount;
    [SerializeField] private Image ItemBackgroundImage;
    [SerializeField] private Image ItemImage;
    private Button button;
    public ItemQualityDisplayData itemQualityDisplayData { get; private set; }
    public event Action<IData> OnItemQualitySelect;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Select);
    }

    public Graphic targetGraphic
    {
        get
        {
            return button.targetGraphic;
        }
    }

    private void InitAssets()
    {
        if (itemQualityDisplayData == null)
            return;

        InitAssetManager();
    }

    public void Select()
    {
        OnItemQualitySelect?.Invoke(itemQualityDisplayData.iData);
    }

    private void InitAssetManager()
    {
        if (ItemAssetManagerSO != null)
            return;

        ItemAssetManagerSO = instance.ItemAssetManagerSO;
    }

    public void SetIData(ItemQualityDisplayData ItemQualityDisplayData)
    {
        if (itemQualityDisplayData != null)
            return;

        itemQualityDisplayData = ItemQualityDisplayData;
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        InitAssets();

        ItemBackgroundImage.sprite = itemQualityDisplayData.iData.GetRaritySO().ItemQualityBackground;
        ItemImage.sprite = itemQualityDisplayData.iData.GetIcon();
        UpdateDisplayText();
    }

    public void UpdateDisplayText()
    {
        DisplayText.text = LimitText(itemQualityDisplayData.GetDisplayText(), LimitTextAmount);
    }

    private string LimitText(string text, int limitCharacters = 0)
    {
        if (text.Length >= limitCharacters)
        {
            return text.Substring(0, limitCharacters) + '.';
        }

        return text;
    }

    public virtual void OnSelect()
    {
        transform.DOScale(1.1f, 0.075f).SetEase(Ease.InOutSine);
    }

    public virtual void OnDeSelect()
    {
        transform.DOScale(1f, 0.075f).SetEase(Ease.InOutSine);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.05f, 0.075f).SetEase(Ease.InOutSine);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.075f).SetEase(Ease.InOutSine);
    }
}
