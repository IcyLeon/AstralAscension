using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementUI : MonoBehaviour
{
    public Dictionary<ElementsSO, ElementIconDisplay> EID_Dict { get; private set; }
    [SerializeField] private GameObject ElementIconDisplayPrefab;
    [SerializeField] private Transform Parent;
    private IDamageable damageable;
    private ObjectPool<ElementIconDisplay> ObjectPool;

    // Start is called before the first frame update
    void Awake()
    {
        EID_Dict = new();
        damageable = GetComponentInParent<IDamageable>();
        damageable.OnElementEnter += OnElementEnter;
        damageable.OnElementExit += OnElementExit;
    }

    private void Start()
    {
        ObjectPool = new ObjectPool<ElementIconDisplay>(ElementIconDisplayPrefab, Parent, 6);
        ObjectPool.ObjectCreated += ObjectPool_ObjectCreated;
    }

    private void ObjectPool_ObjectCreated(ElementIconDisplay EID)
    {
        if (EID == null)
            return;

        EID.SetElementUI(this);
    }

    private void OnDestroy()
    {
        damageable.OnElementEnter -= OnElementEnter;
        damageable.OnElementExit -= OnElementExit;
        ObjectPool.ObjectCreated -= ObjectPool_ObjectCreated;
    }

    private void OnElementEnter(Elements element)
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
        EID_Dict.Add(e, EID);
    }


    private void OnElementExit(Elements element)
    {
        ElementsSO e = element.elementsSO;
        if (EID_Dict.TryGetValue(e, out ElementIconDisplay elementIconDisplay))
        {
            elementIconDisplay.FadeOut();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
