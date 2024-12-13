using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualitySelection : MonoBehaviour
{
    [SerializeField] private Button RemoveBtn;
    public ItemQualityButton itemQualityButton { get; private set; }
    public event Action<ItemQualityButton> OnRemoveClick;

    private void Awake()
    {
        itemQualityButton = GetComponentInParent<ItemQualityButton>(true);
        RemoveBtn.onClick.AddListener(OnRemove);
    }

    private void OnRemove()
    {
        OnRemoveClick?.Invoke(itemQualityButton);
    }

    public void Select()
    {
        gameObject.SetActive(true);
    }

    public void UnSelect()
    {
        gameObject.SetActive(false);
    }
}
