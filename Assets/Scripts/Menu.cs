using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject _ImageCredits;
    public GameObject _MenuHolder;
    public MenuButton _MenuButton;
    [SerializeField] private Transform _close;
    [SerializeField] private Transform _menuHolder;
    private AudioClip _creditSound;

    float _menuFontSize;
    float _generalFontSize;
    public IEnumerator Initialize()
    {
        _menuFontSize = GameStateManager._Instance._UISettings._MenuFontSize;
        _generalFontSize = GameStateManager._Instance._UISettings._GeneralFontSize;
        GameObject sanatoriumButton = Instantiate(_MenuButton.gameObject, _menuHolder, false);
        sanatoriumButton.GetComponent<MenuButton>()._button.onClick.AddListener(Sanatorio);
        sanatoriumButton.GetComponent<MenuButton>()._LabelText.fontSize = _menuFontSize;
        sanatoriumButton.GetComponent<MenuButton>()._LabelText.text = "Sanatorio";

        GameObject parkOfSouls = Instantiate(_MenuButton.gameObject, _menuHolder, false);
        parkOfSouls.GetComponent<MenuButton>()._button.onClick.AddListener(ParkofSouls);
        parkOfSouls.GetComponent<MenuButton>()._LabelText.fontSize = _menuFontSize;
        parkOfSouls.GetComponent<MenuButton>()._LabelText.text = "Park of Souls";

        GameObject credits = Instantiate(_MenuButton.gameObject, _menuHolder, false);
        credits.GetComponent<MenuButton>()._button.onClick.AddListener(ShowCredits);
        credits.GetComponent<MenuButton>()._LabelText.fontSize = _menuFontSize;
        credits.GetComponent<MenuButton>()._LabelText.text = "Credits";

        GameObject quit = Instantiate(_MenuButton.gameObject, _menuHolder, false);
        quit.GetComponent<MenuButton>()._button.onClick.AddListener(ApplicationQuit);
        quit.GetComponent<MenuButton>()._LabelText.fontSize = _menuFontSize;
        quit.GetComponent<MenuButton>()._LabelText.text = "Quit";

        GameObject closeCredits = Instantiate(_MenuButton.gameObject, _close, false);
        closeCredits.GetComponent<MenuButton>()._button.onClick.AddListener(CloseCredits);
        closeCredits.GetComponent<MenuButton>()._LabelText.fontSize = _generalFontSize;
        closeCredits.GetComponent<MenuButton>()._LabelText.text = "Close";

        yield return null;
    }
    public void Sanatorio()
    {
        GameStateManager._Instance._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadSanatorio(() =>
        {
            GameStateManager._Instance.ChangeGameState(GameStateManager.GameState.Sanatorium);
            GameStateManager._Instance._LoadingScreen.SetLoadingScreen(false);
            gameObject.SetActive(false);
        }));
    }


    public void ParkofSouls()
    {
        GameStateManager._Instance._LoadingScreen.SetLoadingScreen(true);
        StartCoroutine(LoadParkOfSouls(() =>
        {
            GameStateManager._Instance.ChangeGameState(GameStateManager.GameState.ParkOfSouls);
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
        CreditSoundPlay();
        _MenuHolder.SetActive(false);
    }

    public void CloseCredits()
    {
        _ImageCredits.SetActive(false);
        CreditSoundStop();
        _MenuHolder.SetActive(true);
    }


    public void CreditSoundPlay()
    {
        StartCoroutine(
         AddressablesLoader.InstantiateGeneralAsync<AudioClip>("Trypes", onDone: (audioClip, handle) =>
        {
            GameStateManager._Instance.SetEnvironmentAudioSource(audioClip, handle);
        }));
    }

    public void CreditSoundStop()
    {
        GameStateManager._Instance.ReleaseEnvironmentAudioSource();
    }
}




