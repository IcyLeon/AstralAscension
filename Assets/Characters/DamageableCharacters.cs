using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DamageableCharacters : Characters, IDamageable
{
    public event IDamageable.TakeDamageEvent OnTakeDamage;
    public event IDamageable.OnElementChange OnElementEnter;
    public event IDamageable.OnElementChange OnElementExit;

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
        if (GetInflictElementLists() == null)
            return;

        for (int i = 0; i < GetInflictElementLists().Count; i++)
        {
            GetInflictElementLists().ElementAt(i).Value.Update();
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

    protected override void OnEnable()
    {
        base.OnEnable();

    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (characterReuseableData != null)
        {
            characterReuseableData.Reset();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (characterStateMachine != null)
        {
            characterStateMachine.OnDestroy();
        }
    }

    public virtual void TakeDamage(IAttacker source, ElementsSO elementsSO, float BaseDamageAmount, Vector3 HitPosition = default(Vector3))
    {
        if (IsDead())
            return;


        OnTakeDamage?.Invoke(source, elementsSO, BaseDamageAmount);

        ElementalReactionsManager.CallDamageInvoke(this, new ElementalReactionsManager.ElementDamageInfoEvent
        {
            damageAmount = BaseDamageAmount,
            elementsSO = elementsSO,
            source = source,
            hitPosition = HitPosition
        });
    }

    public virtual Vector3 GetCenterBound()
    {
        return transform.position;
    }

    public Dictionary<ElementsSO, Elements> GetInflictElementLists()
    {
        return characterReuseableData.inflictElementList;
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

    public void AddElement(ElementsSO elementSO, Elements elements)
    {
        GetInflictElementLists().Add(elementSO, elements);
        OnElementEnter?.Invoke(elements);
    }

    public void RemoveElement(ElementsSO elementSO, Elements elements)
    {
        GetInflictElementLists().Remove(elementSO);
        OnElementExit?.Invoke(elements);
    }
}
