using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject _PauseMenu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
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
        LoadingScreen.s_Instance.SetLoadingScreen(true);
        GameStateManager._Instance.DestroyFeature();
        AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");
        operation.completed += (asyncoperation) => { LoadingScreen.s_Instance.SetLoadingScreen(false); };
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
        _PauseMenu.SetActive(true);
    }

    private void HideMenu()
    {
        _PauseMenu.SetActive(false);
    }

}
