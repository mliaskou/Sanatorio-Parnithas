using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class SaveXml
{
    public static ListNarrative DeserializeXml()
    {
        var settings = new XmlReaderSettings
        {
            ConformanceLevel = ConformanceLevel.Auto
        };
        string fileName = Path.Combine(Application.dataPath, "Narratives.xml");
        ListNarrative listnarrative;
        XmlSerializer serializer = new XmlSerializer(typeof(ListNarrative));
        using ( XmlReader reader = XmlReader.Create(fileName,settings))
        {
            listnarrative = (ListNarrative)serializer.Deserialize(reader);
        }
        return listnarrative;
    }
}
