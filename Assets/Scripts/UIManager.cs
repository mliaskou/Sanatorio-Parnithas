using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager _Instance;
    private GameObject _narrativeCanvas;

    public NarrativesInventory _NarrativesInventory;
    public Action<GameObject> _ShowNarrativeCanvas;
    public Dictionary<string, string> _NarrativesDict = new Dictionary<string, string>();
    [HideInInspector] public TMP_Text _NarrativesText;

    private void Awake()
    {
        _Instance = this;
        if (_narrativeCanvas == null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("NarrativeCanvas")) as GameObject;
            go.transform.SetParent(this.transform, false);
            go.SetActive(true);
            _narrativeCanvas = go;
        }
        else
        {
            Debug.LogError("It is not null");
        }
        ListNarrative listNarrative = SaveXml.DeserializeXml();
        foreach(Narrative narrative in listNarrative.Narratives)
        {
            _NarrativesDict.Add(narrative.Id, narrative.Description);
        }
        _NarrativesText = _narrativeCanvas.GetComponent<NarrativesText>()._NarrativesText;
        _ShowNarrativeCanvas = ShowNarrativeCanvas;
    }

    public void ShowNarrativeCanvas(GameObject go)
    {
        _narrativeCanvas.transform.SetParent(go.transform, false);
        if (_NarrativesDict.ContainsKey(go.name))
        {
            _NarrativesText.text = _NarrativesDict[go.name];
        }
        _narrativeCanvas.transform.LookAt(_narrativeCanvas.transform.position + GameStateManager._Instance._PlayerCamera.transform.rotation * Vector3.forward, GameStateManager._Instance._PlayerCamera.transform.rotation * Vector3.up);
        _narrativeCanvas.SetActive(true);
    }

    public void OnDestroy()
    {
        Destroy(_narrativeCanvas);
        _NarrativesDict.Clear();
        _ShowNarrativeCanvas = null;
        _Instance = null;
    }
}
