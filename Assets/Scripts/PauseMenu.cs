using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class PauseMenu:MonoBehaviour
{
    private GameObject _menu;
    private Menu _menuScript;
    [SerializeField] private Transform _pauseMenuParent;
    [SerializeField] GameObject _pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
    public IEnumerator Initialise(GameObject menu)
    {
        //Debug.LogError("dgdfgdfg");
        _menu = menu;
        _menuScript = menu.GetComponent<Menu>();
        GameObject resumeButton = Instantiate(_menuScript._MenuButton.gameObject, _pauseMenuParent,false);
        resumeButton.GetComponent<MenuButton>()._button.onClick.AddListener(Resume);
        resumeButton.GetComponent<MenuButton>()._LabelText.text = "Resume";
        resumeButton.GetComponent<MenuButton>()._LabelText.fontSize = 20f;

        GameObject menuButton = Instantiate(_menuScript._MenuButton.gameObject, _pauseMenuParent,false);
        menuButton.GetComponent<MenuButton>()._button.onClick.AddListener(Menu);
        menuButton.GetComponent<MenuButton>()._LabelText.text = "Menu";
        menuButton.GetComponent<MenuButton>()._LabelText.fontSize = 20f;

        GameObject quitButton = Instantiate(_menuScript._MenuButton.gameObject, _pauseMenuParent,false);
        quitButton.GetComponent<MenuButton>()._button.onClick.AddListener(Quit);
        quitButton.GetComponent<MenuButton>()._LabelText.text = "Quit";
        quitButton.GetComponent<MenuButton>()._LabelText.fontSize = 20f;
        yield return null;
    }
    public void Pause()
    {
        GameStateManager._Instance.Pause();
        ShowMenu();
    }
    public void Resume()
    {
        GameStateManager._Instance.Resume();
        HideMenu();
    }
    public void Menu()
    {
        GameStateManager._Instance._LoadingScreen.SetLoadingScreen(true);
        GameStateManager._Instance.ChangeGameState(GameStateManager.GameState.MainMenu);
        _menu.SetActive(true);
        HideMenu();
        GameStateManager._Instance._LoadingScreen.SetLoadingScreen(false);
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
