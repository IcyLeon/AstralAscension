using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffEvent : EventArgs
{
    public BuffEffect BuffEffect;
}

public abstract class BuffEffect
{
    public event EventHandler<BuffEvent> OnBuffRemove;

    public virtual void Update()
    {

    }

    public BuffEffect()
    {
    }
}
