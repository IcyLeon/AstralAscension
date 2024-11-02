using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualitySelection : MonoBehaviour
{
    [SerializeField] private Button RemoveBtn;
    public ItemQualityIEntity itemQualityIEntity { get; private set; }
    public event EventHandler<ItemQualityEvents> OnRemoveClick;

    private void Awake()
    {
        Init();
        RemoveBtn.onClick.AddListener(OnRemove);
    }

    private void Init()
    {
        if (itemQualityIEntity != null)
            return;

        itemQualityIEntity = GetComponentInParent<ItemQualityIEntity>(true);
    }

    private void OnRemove()
    {
        OnRemoveClick?.Invoke(this, new ItemQualityEvents
        {
            ItemQualityButton = itemQualityIEntity
        });
    }

    public void RevealSelection()
    {
        Init();
        gameObject.SetActive(true);

        itemQualityIEntity.HideNewStatus();
    }

    public void HideSelection()
    {
        Init();
        gameObject.SetActive(false);
    }
}
