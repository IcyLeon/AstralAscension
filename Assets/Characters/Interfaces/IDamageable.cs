using System.Collections.Generic;
using UnityEngine;

public interface IKnockBack
{
    void KnockBack(Vector3 force);
}
public interface IAttacker
{
    ElementsSO GetElementsSO();
    float GetATK();
    float GetEM();
}

public interface IDamageable : IAttacker
{
    float GetDEF();
    float GetMaxHealth();
    float GetCurrentHealth();
    void SetCurrentHealth(float health);
    ElementsInfoSO[] GetImmuneableElementsInfoSO();
    bool IsDead();
    Vector3 GetCenterBound();
    void TakeDamage(IAttacker source, ElementsInfoSO e, float BaseDamageAmount, Vector3 HitPosition = default(Vector3));

    public delegate void TakeDamageEvent(float DamageAmount);
    public event TakeDamageEvent OnTakeDamage;

    /// <summary>
    /// VERY IMPORTANT FOR EVERY DAMAGEABLE OBJECTS
    /// </summary>
    public void SetCharacterDataStat(CharacterDataStat CharacterDataStat);
    public CharacterDataStat GetCharacterDataStat();
}


