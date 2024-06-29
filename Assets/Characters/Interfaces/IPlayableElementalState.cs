public interface IPlayableElementalState
{
    void OnElementalStateEnter();
    void OnElementalStateExit();
    bool IsElementalStateEnded();
    void UpdateElementalState();
}
