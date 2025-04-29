using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ElementUI : MonoBehaviour
{
    protected Dictionary<ElementsSO, ElementIconDisplay> EID_Dict;
    [SerializeField] private GameObject ElementIconDisplayPrefab;
    [SerializeField] private Transform Parent;
    protected ObjectPool<ElementIconDisplay> ObjectPool;
    public CharacterDataStat characterDataStat { get; private set; }

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        EID_Dict = new();
    }

    private void Start()
    {
        ObjectPool = new ObjectPool<ElementIconDisplay>(ElementIconDisplayPrefab, Parent, 6);
    }

    public void SetCharacterDataStat(CharacterDataStat c)
    {
        characterDataStat = c;
    }

    protected virtual void OnDestroy()
    {
    }

    protected void OnElementEnter(Elements element)
    {
        ElementsSO e = element.elementsSO;
        if (EID_Dict.TryGetValue(e, out ElementIconDisplay elementIconDisplay))
        {
            elementIconDisplay.FadeIn();
            return;
        }

        ElementIconDisplay EID = ObjectPool.GetPooledObject();

        if (EID == null)
            return;

        EID.SetElementsSO(e);
        EID.OnElementDestroyed += EID_OnElementDestroyed;

        EID_Dict.Add(e, EID);
    }

    private void EID_OnElementDestroyed(ElementsSO eSO)
    {
        EID_Dict.Remove(eSO);
    }

    protected void OnElementExit(Elements element)
    {
        ElementsSO e = element.elementsSO;
        if (EID_Dict.TryGetValue(e, out ElementIconDisplay elementIconDisplay))
        {
            elementIconDisplay.FadeOut();
        }
    }
}
