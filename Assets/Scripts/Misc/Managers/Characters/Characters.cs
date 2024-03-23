using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour, IDamagable
{

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public virtual void TakeDamage(IDamagable source, float BaseDamageAmount)
    {
    }
}
