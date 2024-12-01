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

    private void Awake()
    {
        _Instance = this;
        GetComponent<PositionThePlayer>().InitializePlayerPosition(player);
        _PlayerCamera = player.transform.GetChild(0).GetComponent<Camera>();
        StartCoroutine(_AudioManager.Initialize());
    }

    public void Pause()
    {
        player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        S_isPaused = true;
    }
    public void Resume()
    {
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        S_isPaused = false;
    }

    public IEnumerator DestroyFeature()
    {
        yield return _AudioManager.DestroyFeature();
        _AudioManager = null;
        S_isPaused = false;
        _Instance = null;
    }
}
