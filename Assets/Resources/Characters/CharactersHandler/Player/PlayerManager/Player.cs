using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [field: SerializeField] public PlayerSO PlayerSO { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    public PlayerCameraManager PlayerCameraManager { get; private set; }
    [SerializeField] private AudioSource PlayerSoundSource;
    public PlayerController playerController { get; private set; }
    public ActiveCharacter activeCharacter { get; private set; } 

    // Start is called before the first frame update
    private void Awake()
    {
        playerController = new PlayerController();
        CreatePlayerData();
        PlayerCameraManager = GetComponentInChildren<PlayerCameraManager>();
        activeCharacter = GetComponentInChildren<ActiveCharacter>();
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

    public static Vector3 GetTargetCameraRayPosition(float maxDistance)
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
        return ray.origin + ray.direction * maxDistance;
    }

    public static Vector3 GetRayPosition(Vector3 originPosition, Vector3 direction, float maxDistance)
    {
        if (Physics.Raycast(originPosition, direction.normalized, out RaycastHit hit, maxDistance, ~LayerMask.GetMask("Ignore Raycast")))
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
