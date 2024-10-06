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
    public event EventHandler OnRemoveClick;

    private void Awake()
    {
        itemQualityIEntity = GetComponentInParent<ItemQualityIEntity>(true);
        RemoveBtn.onClick.AddListener(OnRemove);
    }

    private void OnRemove()
    {
        OnRemoveClick?.Invoke(this, EventArgs.Empty);
    }

    public void RevealSelection()
    {
        gameObject.SetActive(true);
    }

    public void HideSelection()
    {
        gameObject.SetActive(false);
    }
}
