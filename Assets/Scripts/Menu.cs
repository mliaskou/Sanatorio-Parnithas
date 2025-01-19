using System.Collections;
using UnityEngine;
using System;

public class Menu : MonoBehaviour
{
    public GameObject _ImageCredits;
    public GameObject _MenuHolder;
    [SerializeField] AudioSource creditsSound;

    public void Sanatorio()
    {
        GameStateManager._Instance._UIManager._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadSanatorio(() =>
        {
            GameStateManager._Instance._UIManager._LoadingScreen.SetLoadingScreen(false);
            gameObject.SetActive(false);
        }));
    }


    public void ParkofSouls()
    {
        GameStateManager._Instance._UIManager._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadParkOfSouls(() =>
        {
            GameStateManager._Instance._UIManager._LoadingScreen.SetLoadingScreen(false);
            gameObject.SetActive(false);
        }));
    }

    IEnumerator LoadSanatorio(Action onComplete)
    {
        yield return GameStateManager._Instance.GetComponent<PositionThePlayer>().InitializePlayerPosition(true);
        GameStateManager._Instance._UIManager._NarrativeInventoryGameObject.SetActive(true);
        onComplete?.Invoke();
        yield return null;
    }


    IEnumerator LoadParkOfSouls(Action onComplete)
    {
        yield return GameStateManager._Instance.GetComponent<PositionThePlayer>().InitializePlayerPosition(false);
        GameStateManager._Instance._UIManager._NarrativeInventoryGameObject.SetActive(true);
        onComplete?.Invoke();
        yield return null;
    }

    private void OnDestroy()
    {
        UnityEngine.AddressableAssets.Addressables.Release(gameObject);
    }

    public void ApplicationQuit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ShowCredits()
    {
        _ImageCredits.SetActive(true);
        _MenuHolder.SetActive(false);
    }

    public void CloseImage()
    {
        _ImageCredits.SetActive(false);
        _MenuHolder.SetActive(true);
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




