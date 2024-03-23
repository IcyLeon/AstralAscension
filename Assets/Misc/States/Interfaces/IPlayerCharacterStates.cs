public interface IPlayerCharacterStates : IState
{
    void OnElementalSkillCast();
    void OnElementalBurstCast();
    void OnAttackCast();
}
