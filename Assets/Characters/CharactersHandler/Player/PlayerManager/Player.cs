using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Collision Events
    public delegate void OnCollisionEvent(Collision collision);
    public OnCollisionEvent OnCollisionEnterEvent;
    public OnCollisionEvent OnCollisionStayEvent;
    public OnCollisionEvent OnCollisionExitEvent;
    #endregion

    [field: SerializeField] public PlayerSO PlayerSO { get; private set; }
    [field: SerializeField] public Rigidbody Rb { get; private set; }
    [field: SerializeField] public CameraManager CameraManager { get; private set; }

    [SerializeField] private AudioSource PlayerSoundSource;

    public PlayerData playerData { get; private set; }

    private PlayerInput playerInput;

    // Start is called before the first frame update
    private void Awake()
    {
        playerData = new PlayerData(this);
        playerInput = GetComponent<PlayerInput>();
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

    private IEnumerator DisableInputCoroutine(InputAction PA, float sec)
    {
        PA.Disable();
        yield return new WaitForSeconds(sec);
        PA.Enable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnCollisionEnterEvent?.Invoke(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        OnCollisionStayEvent?.Invoke(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        OnCollisionExitEvent?.Invoke(collision);
    }

    public PlayerInputSystem.PlayerActions playerInputAction
    {
        get
        {
            return playerInput.playerInputSystem.Player;
        }
    }
}
