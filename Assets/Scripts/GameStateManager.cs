using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using System.Collections;


public class GameStateManager : MonoBehaviour
{
    public static GameStateManager _Instance;
    public static bool S_isPaused = false;
    public GameObject player;
    public UISettings _UISettings;
    [HideInInspector] public Camera _PlayerCamera;
    [HideInInspector] public AudioManager _AudioManager;

    private PlayerController _playerController;
    private FirstPersonController _firstPersonController;

    private GameObject _uiManagerCanvas;
    private GameObject _loadingScreen;
    [HideInInspector] public LoadingScreen _LoadingScreen;
    [HideInInspector] public UIManager _UIManager;
    [HideInInspector] public AudioSource _NarrativeAudioSource;

    private void Awake()
    {
        _Instance = this;
        AddressablesLoader.InstantiateSyncGameObject("LoadingScreenCanvas", (gameObject) =>
        {
            _loadingScreen=gameObject;
            _LoadingScreen = new LoadingScreen(Instantiate(gameObject));
        });
        _NarrativeAudioSource = gameObject.AddComponent<AudioSource>();
    }
    private IEnumerator Start()
    {
        yield return AddressablesLoader.InstantiateGameObject("UIManager", (gameObject) =>
        {
            _uiManagerCanvas = gameObject;           
        });
        GetComponent<PositionThePlayer>().SetDepedencies(player, Resume);
        _PlayerCamera = player.transform.GetChild(0).GetComponent<Camera>();
        _playerController = player.transform.GetChild(0).GetComponent<PlayerController>();
        _firstPersonController = player.GetComponent<FirstPersonController>();
        Pause();
        _UIManager = new UIManager(_uiManagerCanvas);
        yield return _UIManager.Initialize();
        _AudioManager = new AudioManager();
        yield return _AudioManager.Initialize();
        _AudioManager._ShowAndIncreaseCountText = _UIManager.IncreaseAndDisplayCountText;


        if (_UIManager._interactable!=null)
        {
            yield return _playerController.Initialise(_UIManager._interactable);
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

    public void  OnDestroy()
    {
        UnityEngine.AddressableAssets.Addressables.ReleaseInstance(_uiManagerCanvas);
        UnityEngine.AddressableAssets.Addressables.ReleaseInstance(_loadingScreen);
        _AudioManager._ShowAndIncreaseCountText = null;
        _AudioManager = null;
        S_isPaused = false;
        _Instance = null;
    }
}
