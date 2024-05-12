using System.Collections.Generic;

public abstract class CharacterReuseableData
{
    public Dictionary<ElementsSO, Elements> inflictElementList;
    protected CharacterStateMachine characterStateMachine;
    public CharacterReuseableData(CharacterStateMachine characterStateMachine)
    {
        this.characterStateMachine = characterStateMachine;
        inflictElementList = new();
    }

    public virtual void Update()
    {

    }

    public void Reset()
    {
        inflictElementList.Clear();
    }

    public virtual void OnDestroy()
    {

    }
}
