using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public static Menu s_Instance;
    public static bool s_IsLoaded;

    public GameObject _ImageCredits;
    public GameObject _MenuCanvas;
   
    public GameObject _LoadingScreen;
    [SerializeField] AudioSource creditsSound;
    void Awake()
    {
        GameObject loading = Instantiate(_LoadingScreen);
        loading.SetActive(false);
    }

    public void Sanatorio()
    {
        StartCoroutine(LoadSanatorio());
    }


    public void ParkofSouls()
    {
        StartCoroutine(LoadParkOfSouls());

    }
    IEnumerator LoadSanatorio()
    {
        LoadingScreen.s_Instance.SetLoadingScreen(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        PositionThePlayer.S_IsLoaded = true;
        asyncLoad.completed += (AsyncOperation) =>
        {      
            LoadingScreen.s_Instance.SetLoadingScreen(false);
        };
        yield return null;
    }


    IEnumerator LoadParkOfSouls()
    {
        LoadingScreen.s_Instance.SetLoadingScreen(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");
        PositionThePlayer.S_IsLoaded = false;
        asyncLoad.completed += (AsyncOperation) =>
        {       
            LoadingScreen.s_Instance.SetLoadingScreen(false);
        };
        yield return null;
    }

    public void ApplicationQuit()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        _ImageCredits.SetActive(true);
    }

    public void CloseImage()
    {
        _ImageCredits.SetActive(false);
    }


    public void CreditSoundPlay()
    {
        creditsSound.Play();
    }

    public void CreditSoundStop()
    {
        creditsSound.Stop();
    }
}




