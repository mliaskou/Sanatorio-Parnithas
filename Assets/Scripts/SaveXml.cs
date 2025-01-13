using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveXml
{
    private TextAsset _narrativeXml;
    public IEnumerator DeserializeXml(Action<ListNarrative> onComplete)
    {
        yield return AddressablesLoader.InstantiateGeneralAsync<TextAsset>("Narratives", (narrativeXml) =>
        {
            _narrativeXml = narrativeXml;
            string narrativeContent = narrativeXml.text.Trim();

            ListNarrative listnarrative;
            XmlSerializer serializer = new XmlSerializer(typeof(ListNarrative));
            using (TextReader reader = new StringReader(narrativeContent)) {
                listnarrative = (ListNarrative)serializer.Deserialize(reader);
                onComplete?.Invoke(listnarrative);
            }
        });
    }

    public void DestroyFeature()
    {
        UnityEngine.AddressableAssets.Addressables.Release(_narrativeXml);
    }

}
