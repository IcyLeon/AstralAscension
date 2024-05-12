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
    Dictionary<ElementsSO, Elements> GetInflictElementLists();
    ElementsSO GetElementsSO();
    ElementsSO[] GetImmuneableElementsSO();
    bool IsDead();
    Vector3 GetCenterBound();
    void TakeDamage(IAttacker source, ElementsSO elementsSO, float BaseDamageAmount, Vector3 HitPosition = default(Vector3));

    public delegate void TakeDamageEvent(IAttacker source, float DamageAmount);
    public event TakeDamageEvent OnTakeDamage;
}

public interface IAttacker
{
    float GetATK();
    float GetEM();
}
