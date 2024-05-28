using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DamageableCharacters : Characters, IDamageable, IKnockBack
{
    public event IDamageable.TakeDamageEvent OnTakeDamage;

    public CharacterDataStat characterDataStat { get; private set; }

    protected CharacterStateMachine characterStateMachine;

    protected DamageableEntitySO damageableCharacters
    {
        get
        {
            return CharacterSO as DamageableEntitySO;
        }
    }

    public CharacterDataStat GetCharacterDataStat()
    {
        if (characterDataStat == null)
        {
            characterDataStat = new CharacterDataStat(CharacterSO);
        }

        return characterDataStat;
    }

    protected override void Awake()
    {
        base.Awake();

    }

    protected override void Start()
    {
        base.Start();
        if (characterStateMachine == null)
        {
            characterStateMachine = CreateCharacterStateMachine();
        }
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

        GetCharacterDataStat().Update();
    }
    public void OnAnimationTransition()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnAnimationTransition();
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

    protected abstract CharacterStateMachine CreateCharacterStateMachine();

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

        ElementalReactionsManager.CallDamageInvoke(this, new ElementalReactionsManager.ElementDamageInfoEvent
        {
            damageAmount = BaseDamageAmount,
            elementsInfoSO = e,
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
        if (characterDataStat == null)
            return null;

        return characterDataStat.inflictElementList;
    }

    public virtual float GetMaxHealth()
    {
        return characterDataStat.maxHealth;
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
        return characterDataStat.currentHealth;
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
        GetCharacterDataStat().AddElement(elementSO, elements);
    }

    public void RemoveElement(ElementsSO elementSO, Elements elements)
    {
        GetCharacterDataStat().RemoveElement(elementSO, elements);
    }

    public virtual void KnockBack(Vector3 force)
    {
    }
}
