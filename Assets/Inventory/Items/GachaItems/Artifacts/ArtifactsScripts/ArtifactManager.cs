using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;

public class ArtifactManager : MonoBehaviour
{
    [field: SerializeField] public ArtifactManagerSO ArtifactManagerSO { get; private set; }

    private CharacterStorage characterStorage;
    public static ArtifactManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
    }

    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
        if (characterStorage != null)
        {
        }
    }

    public void RemoveArtifacts(CharactersSO characterSO, Artifact artifact)
    {
        if (artifact == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
            return;

        characterStorage.playableCharacterStatList[characterSO].RemoveArtifacts(artifact.GetItemType());
    }

    public void AddArtifacts(CharactersSO characterSO, Artifact artifact)
    {
        if (artifact == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
            return;

        CharactersSO previousOwnerSO = artifact.equipByCharacter;

        RemoveArtifacts(previousOwnerSO, artifact); // remove previous owner of the artifact

        Artifact CurrentArtifactEquipped = characterStorage.playableCharacterStatList[characterSO].GetItem(artifact.GetItemType()) as Artifact;
        AddArtifacts(previousOwnerSO, CurrentArtifactEquipped);

        if (CurrentArtifactEquipped != null) // swap
        {
            CurrentArtifactEquipped.SetEquip(previousOwnerSO);
        }

        characterStorage.playableCharacterStatList[characterSO].AddArtifacts(artifact); // new owner of the artifact


    }

    private void OnDestroy()
    {
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
