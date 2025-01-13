using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager _Instance;
    public static bool S_isPaused = false;
    public GameObject player;
    [HideInInspector] public Camera _PlayerCamera;
    public AudioManager _AudioManager;
    public UIManager _UIManager;

    private PlayerController _playerController;
    private FirstPersonController _firstPersonController;

    private void Awake()
    {
        _Instance = this;      
    }
    private IEnumerator Start()
    {
        _UIManager = UIManager._Instance;
         GetComponent<PositionThePlayer>().SetDepedencies(player, Resume);
        _PlayerCamera = player.transform.GetChild(0).GetComponent<Camera>();
        _playerController = player.transform.GetChild(0).GetComponent<PlayerController>();
        _firstPersonController = player.GetComponent<FirstPersonController>();
        Pause();
        yield return _UIManager.Initialize();
        yield return _AudioManager.Initialize();
        _AudioManager._ShowAndIncreaseCountText = _UIManager.IncreaseAndDisplayCountText;


        if (_UIManager._InteractableText!=null)
        {
            yield return _playerController.Initialise(_UIManager._InteractableText);
        }
        else
        {
            Debug.LogError("It is null");
        }
        yield return null;
    }
    public void Pause()
    {
        _firstPersonController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        S_isPaused = true;
    }
    public void Resume()
    {
        _firstPersonController.enabled = true;
        Cursor.visible = false;
        S_isPaused = false;
    }

    public IEnumerator DestroyFeature()
    {
        yield return _AudioManager.DestroyFeature();
        _AudioManager._ShowAndIncreaseCountText = null;
        _AudioManager = null;
        S_isPaused = false;
        _Instance = null;
    }
}
