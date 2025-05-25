using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject _ImageCredits;
    public GameObject _MenuHolder;
    private AudioSource _audioSource;
    [SerializeField] private MenuButton _menuButton;
    [SerializeField] private Transform _menuHolder;
    public IEnumerator Initialize()
    {
        GameObject sanatoriumButton = Instantiate(_menuButton.gameObject, _menuHolder,false);
        sanatoriumButton.GetComponent<MenuButton>()._button.onClick.AddListener(Sanatorio);
        sanatoriumButton.GetComponent<MenuButton>()._LabelText.text = "Sanatorio";

        GameObject parkOfSouls = Instantiate(_menuButton.gameObject, _menuHolder,false);
        parkOfSouls.GetComponent<MenuButton>()._button.onClick.AddListener(ParkofSouls);
        parkOfSouls.GetComponent<MenuButton>()._LabelText.text = "Park of Souls";

        GameObject credits = Instantiate(_menuButton.gameObject, _menuHolder,false);
        credits.GetComponent<MenuButton>()._button.onClick.AddListener(ShowCredits);
        credits.GetComponent<MenuButton>()._LabelText.text = "Credits";

        GameObject quit = Instantiate(_menuButton.gameObject, _menuHolder,false);
        quit.GetComponent<MenuButton>()._button.onClick.AddListener(ApplicationQuit);
        quit.GetComponent<MenuButton>()._LabelText.text = "Quit";

        yield return null;
    }
    public void Sanatorio()
    {
        GameStateManager._Instance._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadSanatorio(() =>
        {
            GameStateManager._Instance._LoadingScreen.SetLoadingScreen(false);
            gameObject.SetActive(false);
        }));
    }


    public void ParkofSouls()
    {
        GameStateManager._Instance._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadParkOfSouls(() =>
        {
            GameStateManager._Instance._LoadingScreen.SetLoadingScreen(false);
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
        UnityEngine.AddressableAssets.Addressables.ReleaseInstance(gameObject);
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
        if (_audioSource == null)
        {
            _audioSource = GameStateManager._Instance._NarrativeAudioSource;
            _audioSource.clip = Resources.Load<AudioClip>("Sounds/Trypes");
        }
        
        _audioSource.Play();
    }

    public void CreditSoundStop()
    {
        _audioSource.Stop();
    }
}




