using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager _Instance;

    public Action<GameObject, string> _ShowNarrativeCanvas;
    public Dictionary<string, string> _NarrativesDict = new Dictionary<string, string>();
    private TMP_Text _NarrativesText;
    private TMP_Text _NarrativesNameText;
    [HideInInspector] public LoadingScreen _LoadingScreen;

    GameObject menu;
    GameObject _narrativeCanvas;
    NarrativesInventory _NarrativesInventory;
    GameObject _NarrativeInventoryGameObject;

    [Header("Player")]
    public Text _InteractableText;

    private void Awake()
    {
        _Instance = this;
    }

    public IEnumerator Initialize()
    {
        yield return AddressablesLoader.InstantiateGameObject("LoadingScreenCanvas", (gameObject) =>
        {
            _LoadingScreen = new LoadingScreen(gameObject);
        });

        yield return AddressablesLoader.InstantiateGameObject("menu", (gameObject) =>
        {
            menu = gameObject;
            menu.transform.SetAsLastSibling();
            _LoadingScreen.SetLoadingScreen(false);
        });

        yield return AddressablesLoader.InstantiateGameObject("PauseMenuHolder", (gameObject) => {

            gameObject.transform.SetParent(this.transform, false);
            gameObject.GetComponent<PauseMenu>().Initialise(menu);
        });

        yield return AddressablesLoader.InstantiateGameObject("NarrativeCanvas", (gameObject) =>
        {
            gameObject.transform.SetParent(gameObject.transform, false);
            gameObject.SetActive(false);
            _narrativeCanvas = gameObject;
        });

        yield return AddressablesLoader.InstantiateGameObject("NarrativeInventory", (gameObject) =>
        {
            gameObject.transform.SetParent(gameObject.transform, false);
            gameObject.SetActive(false);
            _NarrativeInventoryGameObject = gameObject;
            _NarrativesInventory = _NarrativeInventoryGameObject.GetComponent<NarrativesInventory>();
            _NarrativesNameText = _NarrativesInventory._NarrativesNameText;
        });

        ListNarrative listNarrative = SaveXml.DeserializeXml();
        foreach (Narrative narrative in listNarrative.Narratives)
        {
            _NarrativesDict.Add(narrative.Id, narrative.Description);
        }
        _NarrativesText = _narrativeCanvas.GetComponent<NarrativesText>()._NarrativesText;
        _ShowNarrativeCanvas = ShowNarrativeCanvas;
        yield return null;
    }
    public void ShowNarrativeCanvas(GameObject go, string name)
    {
        _NarrativesNameText.text = name;

        _NarrativesNameText.gameObject.SetActive(true);
        if (_NarrativesDict.ContainsKey(go.name))
        {
            _narrativeCanvas.transform.SetParent(go.transform, false);
            _NarrativesText.text = _NarrativesDict[go.name];
            _narrativeCanvas.SetActive(true);
        }
        else
        {
            _narrativeCanvas.SetActive(false);
        }
        _narrativeCanvas.transform.LookAt(_narrativeCanvas.transform.position + GameStateManager._Instance._PlayerCamera.transform.rotation * Vector3.forward, GameStateManager._Instance._PlayerCamera.transform.rotation * Vector3.up);
    }

    public void IncreaseAndDisplayCountText(int count)
    {
        _NarrativesInventory._CountText.text = count.ToString();
    }

    public void OnDestroy()
    {
        UnityEngine.AddressableAssets.Addressables.Release(_narrativeCanvas);
        UnityEngine.AddressableAssets.Addressables.Release(_NarrativeInventoryGameObject);
        _LoadingScreen.DestroyFeature();
        _NarrativesDict.Clear();
        _ShowNarrativeCanvas = null;
        _Instance = null;
    }
}
