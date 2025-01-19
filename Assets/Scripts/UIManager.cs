using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager
{
    public Action<GameObject, string> _ShowNarrativeCanvas;
    public Dictionary<string, string> _NarrativesDict = new Dictionary<string, string>();
    private TMP_Text _NarrativesText;
    private TMP_Text _NarrativesNameText;
    [HideInInspector] public SaveXml _SaveXml;

    GameObject menu;
    GameObject _narrativeCanvas;
    NarrativesInventory _NarrativesInventory;
    GameObject _uiCanvas;

    [HideInInspector] public GameObject _NarrativeInventoryGameObject;

    [Header("Player")]
    [HideInInspector] public GameObject _interactable;

    public UIManager(GameObject uiCanvas)
    {
        _uiCanvas = uiCanvas;
    }
    public IEnumerator Initialize()
    {

        yield return AddressablesLoader.InstantiateGameObject("Interactable", (gameObject) =>
        {           
            _interactable = gameObject;
            _interactable.gameObject.SetActive(false);
            _interactable.transform.SetParent(_uiCanvas.transform, false);
        });
    

        yield return AddressablesLoader.InstantiateGameObject("menu", (gameObject) =>
        {
            menu = gameObject;
            menu.transform.SetAsLastSibling();
            GameStateManager._Instance._LoadingScreen.SetLoadingScreen(false);
        });

        yield return AddressablesLoader.InstantiateGameObject("PauseMenuHolder", (gameObject) => {

            gameObject.transform.SetParent(_uiCanvas.transform, false);
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
            _NarrativeInventoryGameObject.transform.SetParent(_uiCanvas.transform, false);
            _NarrativesInventory = _NarrativeInventoryGameObject.GetComponent<NarrativesInventory>();
            _NarrativesNameText = _NarrativesInventory._NarrativesNameText;
        });
        _SaveXml = new SaveXml();
        yield return  _SaveXml.DeserializeXml<ListNarrative>("Narratives",(listNarrative)=> {
            foreach (Narrative narrative in listNarrative.Narratives)
            {
                _NarrativesDict.Add(narrative.Id, narrative.Description);
            }
        });
        _NarrativesText = _narrativeCanvas.GetComponent<NarrativesText>()._NarrativesText;
        _ShowNarrativeCanvas = ShowNarrativeCanvas;
        yield return null;
    }
    public void ShowNarrativeCanvas(GameObject go, string name)
    {
        _NarrativesNameText.text = name;
  
        _NarrativesNameText.gameObject.SetActive(true);
        if (_NarrativesDict.ContainsKey(name))
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
        GameStateManager._Instance._LoadingScreen.DestroyFeature();
        _SaveXml.DestroyFeature();
        _SaveXml = null;
        _NarrativesDict.Clear();
        _ShowNarrativeCanvas = null;
    }
}
