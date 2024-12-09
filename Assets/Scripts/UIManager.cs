using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public static UIManager _Instance;
    private GameObject _narrativeCanvas;

    public NarrativesInventory _NarrativesInventory;
    public Action<GameObject,string> _ShowNarrativeCanvas;
    public Dictionary<string, string> _NarrativesDict = new Dictionary<string, string>();
    [HideInInspector] public TMP_Text _NarrativesText;
    public TMP_Text _NarrativesNameText;

    [Header("Player")]
    public Text _InteractableText;
    private void Awake()
    {
        _Instance = this;
    }

    public IEnumerator Initialize(Action onComplete)
    {
        if (_narrativeCanvas == null)
        {
            GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("NarrativeCanvas")) as GameObject;
            go.transform.SetParent(this.transform, false);
            go.SetActive(false);
            _narrativeCanvas = go;
        }
        else
        {
            Debug.LogError("It is not null");
        }
        ListNarrative listNarrative = SaveXml.DeserializeXml();
        foreach (Narrative narrative in listNarrative.Narratives)
        {
            _NarrativesDict.Add(narrative.Id, narrative.Description);
        }
        _NarrativesText = _narrativeCanvas.GetComponent<NarrativesText>()._NarrativesText;
        _ShowNarrativeCanvas = ShowNarrativeCanvas;
        onComplete?.Invoke();
        yield return null;
    }
    public void ShowNarrativeCanvas(GameObject go,string name)
    {
        _NarrativesNameText.text = name;
        Debug.LogError(_NarrativesNameText);
        _NarrativesNameText.gameObject.SetActive(true);
        if (_NarrativesDict.ContainsKey(go.name))
        {
            _narrativeCanvas.transform.SetParent(go.transform, false);               
            _narrativeCanvas.SetActive(true);
        }
        else
        {
            _narrativeCanvas.SetActive(false);
        }
        _narrativeCanvas.transform.LookAt(_narrativeCanvas.transform.position + GameStateManager._Instance._PlayerCamera.transform.rotation * Vector3.forward, GameStateManager._Instance._PlayerCamera.transform.rotation * Vector3.up);    
    }

    public void OnDestroy()
    {
        Destroy(_narrativeCanvas);
        _NarrativesDict.Clear();
        _ShowNarrativeCanvas = null;
        _Instance = null;
    }
}
