using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SaveXml
{
   public static ListNarrative DeserializeXml()
   {
        string fileName = Path.Combine(Application.dataPath, "Narratives.xml");
        ListNarrative listnarrative;
        XmlSerializer serializer = new XmlSerializer(typeof(ListNarrative));
        using (var stream = File.Open(fileName, FileMode.Open))
        {
            listnarrative = (ListNarrative)serializer.Deserialize(stream);
        }
        return listnarrative;
    }
}
