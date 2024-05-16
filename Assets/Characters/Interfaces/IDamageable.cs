using System.Collections.Generic;
using UnityEngine;

public interface IKnockBack
{
    void KnockBack(Vector3 force);
}

public interface IDamageable : IAttacker
{
    float GetDEF();
    float GetMaxHealth();
    float GetCurrentHealth();
    void SetCurrentHealth(float health);

    /// <summary>
    /// Do not use the dictionary Add or Remove, use AddElement() or RemoveElement() to do the adding or removing of element in the dictionary
    /// </summary>
    /// <returns></returns>
    Dictionary<ElementsSO, Elements> GetInflictElementLists();
    void AddElement(ElementsSO elementSO, Elements elements);
    void RemoveElement(ElementsSO elementSO, Elements elements);

    ElementsSO GetElementsSO();
    ElementsSO[] GetImmuneableElementsSO();
    bool IsDead();
    Vector3 GetCenterBound();
    void TakeDamage(IAttacker source, ElementsSO elementsSO, float BaseDamageAmount, Vector3 HitPosition = default(Vector3));

    public delegate void TakeDamageEvent(IAttacker source, ElementsSO elementsSO, float DamageAmount);
    public event TakeDamageEvent OnTakeDamage;

    public delegate void OnElementChange(Elements element);
    public event OnElementChange OnElementEnter;
    public event OnElementChange OnElementExit;
}

public interface IAttacker
{
    float GetATK();
    float GetEM();
}
