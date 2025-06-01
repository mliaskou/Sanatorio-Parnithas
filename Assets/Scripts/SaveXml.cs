using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SaveXml
{
    private List<AsyncOperationHandle> _narrativeJsonList= new List<AsyncOperationHandle>();
    public IEnumerator DeserializeXml<T>(string xmlName, Action<T> onComplete)where T:class
    {
        yield return AddressablesLoader.InstantiateGeneralAsync<TextAsset>(xmlName,(TextAsset)=>{}, onDone:(narrativeJson, handle) =>
        {
            _narrativeJsonList.Add(handle);
            string narrativeContent = narrativeJson.text.Trim();

            T ListNarrative = null;
            Debug.LogError(ListNarrative);
            T narrativeText = JsonConvert.DeserializeObject<T>(narrativeContent);
            onComplete?.Invoke(narrativeText);
        });
        
    }

    public void DestroyFeature()
    {
        foreach(AsyncOperationHandle handle in _narrativeJsonList)
        {
            UnityEngine.AddressableAssets.Addressables.Release(handle);
        }      
    }
}
