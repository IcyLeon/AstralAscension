using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamageableStats
{
    public float HP;
    public float ATK;
    public float DEF;

    public DamageableStats()
    {
        HP = 0f;
        ATK = 0f;
        DEF = 0f;
    }
}

public abstract class DamageableCharacters : Characters, IDamageable
{
    protected Dictionary<ElementsSO, Elements> inflictElementList;

    protected DamageableStats DamageableStats;
    public delegate void OnDamage(IElement source);
    public OnDamage OnDamageHit;

    public CharacterStateMachine characterStateMachine { get; protected set; }

    public CharacterReuseableData characterReuseableData
    {
        get
        {
            return characterStateMachine.characterReuseableData;
        }
    }

    protected DamageableEntitySO damageableCharacters
    {
        get
        {
            return CharacterSO as DamageableEntitySO;
        }
    }

    protected override void Start()
    {
        inflictElementList = new();
        base.Start();
    }

    public virtual ElementsSO GetElementsSO()
    {
        return damageableCharacters.ElementSO;
    }

    protected override void Update()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.Update();
        }

        UpdateElementList();
    }

    private void UpdateElementList()
    {
        if (GetSelfInflictElementLists() == null)
            return;

        for (int i = 0; i < GetSelfInflictElementLists().Count; i++)
        {
            GetSelfInflictElementLists().ElementAt(i).Value.Update();
        }

    }

    protected override void FixedUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.FixedUpdate();
        }
    }

    protected override void LateUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.LateUpdate();
        }
    }

    public virtual bool IsDead()
    {
        return false;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (characterStateMachine != null)
        {
            characterStateMachine.OnDestroy();
        }
    }

    public virtual void TakeDamage(IElement source, ElementsSO elementsSO, float BaseDamageAmount, Vector3 HitPosition = default(Vector3))
    {
        if (IsDead())
            return;

        OnDamageHit?.Invoke(source);

        ElementalReactionsManager.CallDamageInvoke(this, new ElementalReactionsManager.ElementsInfo
        {
            DamageText = new() 
            { 
                { BaseDamageAmount.ToString() } 
            },
            WorldPosition = GetMiddleBound(),
            DamageAmount = BaseDamageAmount,
            elementsSO = elementsSO,
            source = source,
            HitPosition = HitPosition
        });
    }

    public virtual Vector3 GetMiddleBound()
    {
        return transform.position;
    }

    public Dictionary<ElementsSO, Elements> GetSelfInflictElementLists()
    {
        return inflictElementList;
    }

    public virtual float GetMaxHealth()
    {
        return 0f;
    }

    public virtual float GetATK()
    {
        return 0f;
    }

    public virtual float GetDEF()
    {
        return 0f;
    }

    public virtual float GetCurrentHealth()
    {
        return 0f;
    }

    public virtual void SetCurrentHealth(float health)
    {
    }

    public virtual float GetEM()
    {
        return 0f;
    }

    public virtual ElementsSO[] GetImmuneableElementsSO()
    {
        return null;
    }
}
