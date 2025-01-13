using System.Collections;
using UnityEngine;
using System;
using UnityEngine.AddressableAssets;

public class Menu : MonoBehaviour
{
    public GameObject _ImageCredits;
    [SerializeField] AudioSource creditsSound;

    public void Sanatorio()
    {
        UIManager._Instance._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadSanatorio(() =>
        {
            UIManager._Instance._LoadingScreen.SetLoadingScreen(false);
            gameObject.SetActive(false);
        }));
    }


    public void ParkofSouls()
    {
        UIManager._Instance._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadParkOfSouls(() =>
        {
            UIManager._Instance._LoadingScreen.SetLoadingScreen(false);
            gameObject.SetActive(false);
        }));
    }

    IEnumerator LoadSanatorio(Action onComplete)
    {
        yield return GameStateManager._Instance.GetComponent<PositionThePlayer>().InitializePlayerPosition(true);
        UIManager._Instance._NarrativeInventoryGameObject.SetActive(true);
        onComplete?.Invoke();
        yield return null;
    }


    IEnumerator LoadParkOfSouls(Action onComplete)
    {
        yield return GameStateManager._Instance.GetComponent<PositionThePlayer>().InitializePlayerPosition(false);
        UIManager._Instance._NarrativeInventoryGameObject.SetActive(true);
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




