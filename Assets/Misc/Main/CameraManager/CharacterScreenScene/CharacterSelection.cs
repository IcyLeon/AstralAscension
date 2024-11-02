using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterManager;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance { get; private set; }
    private CameraRotationAroundTarget cameraRotationAroundTarget;

    public CameraSelectionPanStorage cameraSelectionPanStorage { get; private set; }

    private CharacterStorage characterStorage;

    private void Awake()
    {
        instance = this;

        OnCharacterStorageNew += DisplayCharacterSelection_OnCharacterStorageNew;
        OnCharacterStorageOld += DisplayCharacterSelection_OnCharacterStorageOld;

        cameraRotationAroundTarget = GetComponentInChildren<CameraRotationAroundTarget>();

        cameraSelectionPanStorage = new CameraSelectionPanStorage(cameraRotationAroundTarget);
    }

    private void DisplayCharacterSelection_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
        CharacterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
    }

    private void DisplayCharacterSelection_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd += CharacterStorage_OnCharacterAdd;
        }
    }

    private void Update()
    {
        cameraSelectionPanStorage.Update();
    }

    private void CharacterStorage_OnCharacterAdd(CharacterDataStat c)
    {

    }

    private void OnDestroy()
    {
        OnCharacterStorageNew -= DisplayCharacterSelection_OnCharacterStorageNew;
        OnCharacterStorageOld -= DisplayCharacterSelection_OnCharacterStorageOld;

        if (characterStorage != null)
        {
            characterStorage.OnCharacterAdd -= CharacterStorage_OnCharacterAdd;
        }
    }
}
