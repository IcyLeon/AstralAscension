using System.Collections.Generic;
using UnityEngine;

public interface IKnockBack
{
    void KnockBack(Vector3 force);
}

public interface IDamageable : IElement
{
    float GetATK();
    float GetDEF();
    float GetMaxHealth();
    float GetCurrentHealth();
    void SetCurrentHealth(float health);
    Dictionary<ElementsSO, Elements> GetSelfInflictElementLists();
    ElementsSO GetElementsSO();
    ElementsSO[] GetImmuneableElementsSO();
    bool IsDead();
    Vector3 GetMiddleBound();
    void TakeDamage(IElement source, ElementsSO elementsSO, float BaseDamageAmount, Vector3 HitPosition = default(Vector3));
}

public interface IElement
{
    float GetEM();
}
