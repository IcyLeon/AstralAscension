using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ArtifactManager : MonoBehaviour
{
    [field: SerializeField] public ArtifactManagerSO ArtifactManagerSO { get; private set; }
    public static ArtifactManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
