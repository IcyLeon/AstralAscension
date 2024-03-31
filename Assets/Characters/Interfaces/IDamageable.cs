public interface IDamageable
{
    bool IsDead();
    void TakeDamage(IDamageable source, float BaseDamageAmount);
}
