using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DamageManager;

public class MessageEvent : EventArgs
{
    public string Message;
}

[DisallowMultipleComponent]
public class PopoutMessageManager : MonoBehaviour
{
    public static event EventHandler<MessageEvent> OnMessageSend;

    [SerializeField] private GameObject PopOutMessagePrefab;
    [SerializeField] private Transform PopoutParentTransform;
    private PopoutMessage popoutMessage;

    private void Awake()
    {
        GameObject popoutGO = Instantiate(PopOutMessagePrefab, PopoutParentTransform);
        popoutMessage = popoutGO.GetComponent<PopoutMessage>();

        OnMessageSend += PopoutMessageManager_OnMessageSend;
    }

    private void PopoutMessageManager_OnMessageSend(object sender, MessageEvent e)
    {
        popoutMessage.SetMessage(e.Message);
    }

    public static void SendPopoutMessage(object sender, string Message)
    {
        OnMessageSend?.Invoke(sender, new MessageEvent
        {
            Message = Message
        });
    }

    private void OnDestroy()
    {
        OnMessageSend -= PopoutMessageManager_OnMessageSend;
    }
}
