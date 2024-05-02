public abstract class CharacterReuseableData
{
    protected CharacterStateMachine characterStateMachine;
    public CharacterReuseableData(CharacterStateMachine characterStateMachine)
    {
        this.characterStateMachine = characterStateMachine;
    }

    public virtual void Update()
    {

    }

    public virtual void OnDestroy()
    {

    }
}
