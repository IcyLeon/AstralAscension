using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LockItemInfoSO", menuName = "ScriptableObjects/ItemManager/LockItemInfoSO")]
public class LockItemInfoSO : ScriptableObject
{
    [Serializable]
    public class LockInfo
    {
        public Color LockBackgroundColor;
        public Color LockImageColor;
        public Sprite LockImage;
    }
    [field: SerializeField] public LockInfo LockedInfo { get; private set; }
    [field: SerializeField] public LockInfo UnlockedInfo { get; private set; }

    public LockInfo GetLockInfo(bool locked)
    {
        LockInfo lockedInfo = LockedInfo;

        if (!locked)
            lockedInfo = UnlockedInfo;

        return lockedInfo;
    }
}
