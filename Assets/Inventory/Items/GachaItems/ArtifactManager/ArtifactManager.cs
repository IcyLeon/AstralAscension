using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ArtifactManager : MonoBehaviour
{
    [field: SerializeField] public ArtifactManagerSO ArtifactManagerSO { get; private set; }
    public static ArtifactManager instance { get; private set; }
    public static int PIECE_COUNT_EVENT { get; private set; } = 2;
    public static float ARTIFACT_LEVEL_EVENT { get; private set; } = 4;

    private void Awake()
    {
        instance = this;
    }
}
