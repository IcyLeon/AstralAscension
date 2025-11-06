using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualitySelection : MonoBehaviour
{
    [SerializeField] private Button RemoveBtn;
    public ItemQuality itemQuality { get; private set; }
    public event Action<ItemQuality> OnRemoveClick;

    private void Awake()
    {
        itemQuality = GetComponentInParent<ItemQuality>(true);
        RemoveBtn.onClick.AddListener(OnRemove);
    }

    private void OnRemove()
    {
        OnRemoveClick?.Invoke(itemQuality);
    }

    public void Select()
    {
        gameObject.SetActive(true);
    }

    public void UnSelect()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        RemoveBtn.onClick.RemoveAllListeners();
    }
}
