using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
    }

    public void RemoveArtifacts(CharactersSO characterSO, Artifact artifact)
    {
        if (artifact == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
            return;

        characterStorage.playableCharacterStatList[characterSO].RemoveArtifacts(artifact.GetTypeSO());
    }

    public void AddArtifacts(CharactersSO characterSO, Artifact artifact)
    {
        if (artifact == null || characterSO == null)
            return;

        if (characterStorage == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
            return;

        // get the existing artifact equipped from characterSO
        Artifact CurrentArtifactEquipped = characterStorage.playableCharacterStatList[characterSO].GetItem(artifact.GetTypeSO()) as Artifact;
        
        CharactersSO previousOwnerSO = artifact.equipByCharacter;
        RemoveArtifacts(previousOwnerSO, artifact); // remove previous owner of the artifact
        characterStorage.playableCharacterStatList[characterSO].AddArtifacts(artifact); // set the new owner of the artifact

        AddArtifacts(previousOwnerSO, CurrentArtifactEquipped); // set the previous owner to the artifact equipped from characterSO 
        if (CurrentArtifactEquipped != null)
        {
            CurrentArtifactEquipped.SetEquip(previousOwnerSO);
        }
    }

    public Dictionary<ArtifactFamilySO, int> GetAllArtifactFamilyInventory(CharactersSO characterSO)
    {
        Dictionary<ArtifactFamilySO, int> artifactFamilySOs = new();

        if (characterSO == null || !characterStorage.playableCharacterStatList.ContainsKey(characterSO))
        {
            return artifactFamilySOs;
        }

        Dictionary<ItemTypeSO, Artifact> artifactList = characterStorage.playableCharacterStatList[characterSO].GetArtifactList();

        foreach (var artifactKeyPair in artifactList)
        {
            ArtifactFamilySO artifactFamilySO = ArtifactManagerSO.GetArtifactFamilySO(artifactKeyPair.Value.GetInterfaceItemReference());

            if (!artifactFamilySOs.ContainsKey(artifactFamilySO))
            {
                artifactFamilySOs.Add(artifactFamilySO, 1);
            }
            else
            {
                artifactFamilySOs[artifactFamilySO]++;
            }
        }

        return artifactFamilySOs;
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
