using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.CompilerServices;


public class Menu : MonoBehaviour
{
    public static Menu s_Instance;
    public static bool s_IsLoaded;

    public GameObject _ImageCredits;
    public GameObject _LoadingScreen;
    public GameObject _MenuCanvas;

    GameObject loading;

    [SerializeField] AudioSource creditsSound;
    void Awake()
    { 
        // if the singleton hasn't been initialized yet
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);

        loading = GameObject.Instantiate(_LoadingScreen);
        loading.transform.SetParent(_MenuCanvas.transform, false);
        loading.SetActive(false);
    }

    public void InitializeMenuUI()
    {

    }
    public void Sanatorio()
    {
        StartCoroutine(LoadSanatorio());
    }


    public void ParkofSouls()
    {
        StartCoroutine(LoadParkOfSouls());
    }
    IEnumerator LoadSanatorio(){
        loading.SetActive(true);
        CreditSoundStop();
        s_IsLoaded = false;
        SceneManager.LoadSceneAsync("SampleScene");   
        yield return null;
        loading.SetActive(false);
    }


    IEnumerator LoadParkOfSouls()
    {
        loading.SetActive(true);
        CreditSoundStop();
        s_IsLoaded = true;
        SceneManager.LoadSceneAsync("SampleScene");
        yield return null;
        loading.SetActive(false);
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




