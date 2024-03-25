using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerManager/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public GroundedData GroundedData { get; private set; }
    [field: SerializeField] public AirborneData AirborneData { get; private set; }
}
