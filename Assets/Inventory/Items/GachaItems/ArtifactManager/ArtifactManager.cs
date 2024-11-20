using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ArtifactManager : MonoBehaviour
{
    [field: SerializeField] public ArtifactManagerSO ArtifactManagerSO { get; private set; }
    public static ArtifactManager instance { get; private set; }
    public static int PIECE_COUNT { get; private set; } = 2;
    public static int ARTIFACT_LEVEL_EVENT { get; private set; } = 4;
    public Dictionary<ArtifactFamilySO, ArtifactEffectFactoryManager> ArtifactEffectFactories { get; private set; }

    private void Awake()
    {
        instance = this;
        ArtifactEffectFactories = new();
    }

    private void Start()
    {
        foreach (var family in ArtifactManagerSO.ArtifactFamilyList)
        {
            ArtifactEffectFactories.Add(family, family.CreateArtifactEffectFactoryManager());
        }
    }
}
