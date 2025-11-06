using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : EventArgs
{
    public string DamageText;
    public ElementsInfoSO ElementsInfoSO;
    public Vector3 WorldPosition;
}

public static class DamageMiscEvent
{
    public static event Action<DamageInfo> OnDamageTextSend;

    public static void SendDamageInfo(DamageInfo d)
    {
        OnDamageTextSend?.Invoke(d);
    }
}
