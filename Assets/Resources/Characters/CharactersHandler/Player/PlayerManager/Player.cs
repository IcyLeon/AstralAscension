using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO PlayerSO { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    public PlayerCameraManager playerCameraManager { get; private set; }
    [SerializeField] private AudioSource PlayerSoundSource;

    private PlayerController controller;
    public PlayerController playerController
    {
        get
        {
            if (controller == null)
            {
                controller = new PlayerController();
            }

            return controller;
        }
    }


    // Start is called before the first frame update
    private void Awake()
    {
        CreatePlayerData();
        playerCameraManager = GetComponentInChildren<PlayerCameraManager>();
    }

    private void OnEnable()
    {
        playerController.OnEnable();
    }

    private void OnDisable()
    {
        playerController.OnDisable();
    }

    private void CreatePlayerData()
    {
        PlayerData playerData = PlayerData.instance;

        if (playerData == null)
            return;

        playerData.SetPlayer(this);
    }


    public void PlayPlayerSoundEffect(AudioClip clip)
    {
        if (clip == null)
            return;

        PlayerSoundSource.PlayOneShot(clip);
    }

    public void DisableInput(InputAction PA, float sec)
    {
        StartCoroutine(DisableInputCoroutine(PA, sec));
    }

    public static Vector3 GetRayPosition(Vector3 originPosition, Vector3 direction, float maxDistance)
    {
        if (Physics.Raycast(originPosition, direction.normalized, out RaycastHit hit, maxDistance, ~LayerMask.GetMask("Ignore Raycast"), QueryTriggerInteraction.Ignore))
        {
            return hit.point;
        }

        return originPosition + direction.normalized * maxDistance;
    }

    private IEnumerator DisableInputCoroutine(InputAction PA, float sec)
    {
        PA.Disable();
        yield return new WaitForSeconds(sec);
        PA.Enable();
    }
}
