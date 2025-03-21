using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageManager;


[DisallowMultipleComponent]
public class PopoutMessageManager : MonoBehaviour
{
    public static event Action<string> OnMessageSend;

    [SerializeField] private GameObject PopOutMessagePrefab;
    [SerializeField] private Transform PopoutParentTransform;
    private PopoutMessage popoutMessage;

    private void Awake()
    {
        GameObject popoutGO = Instantiate(PopOutMessagePrefab, PopoutParentTransform);
        popoutMessage = popoutGO.GetComponent<PopoutMessage>();

        OnMessageSend += PopoutMessageManager_OnMessageSend;
    }

    private void PopoutMessageManager_OnMessageSend(string Message)
    {
        popoutMessage.SetMessage(Message);
    }

    public static void SendPopoutMessage(string Message)
    {
        OnMessageSend?.Invoke(Message);
    }

    private void OnDestroy()
    {
        OnMessageSend -= PopoutMessageManager_OnMessageSend;
    }
}
