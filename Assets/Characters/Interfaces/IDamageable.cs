using UnityEngine;

public interface IDamageable
{
    bool IsDead();
    Vector3 GetMiddleBound();
    void TakeDamage(IDamageable source, float BaseDamageAmount);
}
