using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveXml
{
    private List<TextAsset> _narrativeXmlList= new List<TextAsset>();
    public IEnumerator DeserializeXml<T>(string xmlName, Action<T> onComplete)where T:class
    {
        yield return AddressablesLoader.InstantiateGeneralAsync<TextAsset>(xmlName, (narrativeXml) =>
        {
            _narrativeXmlList.Add(narrativeXml);
            string narrativeContent = narrativeXml.text.Trim();

            T listnarrative;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (TextReader reader = new StringReader(narrativeContent)) {
                listnarrative = (T)serializer.Deserialize(reader);
                onComplete?.Invoke(listnarrative);
            }
        });
    }

    public void DestroyFeature()
    {
        foreach(TextAsset xml in _narrativeXmlList)
        {
            UnityEngine.AddressableAssets.Addressables.Release(xml);
        }      
    }
}
