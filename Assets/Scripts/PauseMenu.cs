using UnityEngine.UI;
using UnityEngine;


public class PauseMenu:MonoBehaviour
{
    private GameObject _menu;
    public Button _Play;
    public Button _Menu;
    public Button _Pause;
    [SerializeField] GameObject _pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
    public void Initialise(GameObject menu)
    {
        //Debug.LogError("dgdfgdfg");
        _menu = menu;
        _Play.onClick.AddListener(() =>
        {
            Resume();
        });

        _Pause.onClick.AddListener(() =>
        {
            Pause();
        });

        _Menu.onClick.AddListener(() =>
        {
            Menu();
        });
    }
    public void Pause()
    {
        Debug.LogError("Pause");
        GameStateManager._Instance.Pause();
        ShowMenu();
    }
    public void Resume()
    {
        Debug.LogError("Resume");
        GameStateManager._Instance.Resume();
        HideMenu();
    }
    public void Menu()
    {
        UIManager._Instance._LoadingScreen.SetLoadingScreen(true);
        _menu.SetActive(true);
        HideMenu();
        UIManager._Instance._LoadingScreen.SetLoadingScreen(false);
    }

    public void Quit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void ShowMenu()
    {
        _pauseMenu.SetActive(true);
    }

    private void HideMenu()
    {
        _pauseMenu.SetActive(false);
    }

    public void DestroyFeature()
    {
        UnityEngine.AddressableAssets.Addressables.Release(gameObject);
    }
}
