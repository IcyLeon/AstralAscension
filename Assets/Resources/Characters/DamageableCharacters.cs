using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DamageableCharacters : Characters, IDamageable, IKnockBack
{
    public event IDamageable.TakeDamageEvent OnTakeDamage;

    private CharacterDataStat characterDataStat;

    protected CharacterStateMachine characterStateMachine;

    protected DamageableEntitySO damageableCharactersSO
    {
        get
        {
            return CharacterSO as DamageableEntitySO;
        }
    }

    public void SetCharacterDataStat(CharacterDataStat CharacterDataStat)
    {
        characterDataStat = CharacterDataStat;
    }

    public virtual CharacterDataStat GetCharacterDataStat()
    {
        if (characterDataStat == null)
        {
            SetCharacterDataStat(damageableCharactersSO.CreateCharacterDataStat());
        }

        return characterDataStat;
    }

    public void CreateCharacterStateMachine()
    {
        if (characterStateMachine != null)
            return;

        characterStateMachine = GetStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        CreateCharacterStateMachine();
    }

    protected override void Awake()
    {
        base.Awake();
        characterDataStat = GetCharacterDataStat();
    }

    public virtual ElementsSO GetElementsSO()
    {
        return damageableCharactersSO.ElementSO;
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (characterStateMachine != null)
        {
            characterStateMachine.Update();
        }

        UpdateDataStat();
    }

    protected virtual void UpdateDataStat()
    {
        GetCharacterDataStat().Update();
    } 

    public void OnAnimationTransition()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnAnimationTransition();
        }
    }

    protected override void OnFixedUpdate()
    {
        base.OnFixedUpdate();

        if (characterStateMachine != null)
        {
            characterStateMachine.FixedUpdate();
        }
    }

    protected override void OnLateUpdate()
    {
        base.OnLateUpdate();

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

        if (characterStateMachine != null)
        {
            characterStateMachine.OnEnable();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        if (characterStateMachine != null)
        {
            characterStateMachine.OnDisable();
        }
    }

    protected abstract CharacterStateMachine GetStateMachine();

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (characterStateMachine != null)
        {
            characterStateMachine.OnDestroy();
        }
    }

    public virtual void TakeDamage(IAttacker source, ElementsInfoSO e, float BaseDamageAmount, Vector3 HitPosition = default(Vector3))
    {
        if (IsDead())
            return;

        OnTakeDamage?.Invoke(BaseDamageAmount);

        ElementalReactionsManager.instance.elementalReactionMiscEvents.TakeDamage(new ElementDamageInfoEvent
        {
            damageAmount = BaseDamageAmount,
            elementsInfoSO = e,
            source = source,
            hitPosition = HitPosition,
            target = this
        });
    }

    public virtual Vector3 GetCenterBound()
    {
        return transform.position;
    }

    public Dictionary<ElementsSO, Elements> GetInflictElementLists()
    {
        if (characterDataStat == null)
            return null;

        return characterDataStat.inflictElementList;
    }

    public virtual float GetMaxHealth()
    {
        return characterDataStat.GetMaxHealth();
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
        return characterDataStat.GetCurrentHealth();
    }

    public virtual void SetCurrentHealth(float health)
    {
        characterDataStat.SetCurrentHealth(health);
    }

    public virtual float GetEM()
    {
        return 0f;
    }

    public virtual ElementsInfoSO[] GetImmuneableElementsInfoSO()
    {
        return null;
    }

    public void AddElement(ElementsSO elementSO, Elements elements)
    {
        characterDataStat.AddElement(elementSO, elements);
    }

    public void RemoveElement(ElementsSO elementSO, Elements elements)
    {
        characterDataStat.RemoveElement(elementSO, elements);
    }

    public virtual void KnockBack(Vector3 force)
    {
    }
}
