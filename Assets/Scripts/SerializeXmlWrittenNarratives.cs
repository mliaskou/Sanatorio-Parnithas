using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Xml;

[Serializable]
[XmlRoot]
public class Narrative
{
    [SerializeField]public string Id = "";
    [SerializeField] public string Description = "";

    public Narrative() { 
    
    }
    public Narrative(string id, string description)
    {
        Id = id;
        Description = description;
    }
}

[XmlRoot]
[Serializable]
public class ListNarrative
{
  [HideInInspector] [SerializeField] public List<Narrative> Narratives = new List<Narrative>();
}

[XmlRoot]
[Serializable]
public class NarrativeIds
{
    [HideInInspector] [SerializeField] public List<string> NarrativeIdsList = new List<string>();
}

public class SerializeXmlWrittenNarratives: MonoBehaviour
{
    string fileName = "";
    public List<Narrative> narratives = new List<Narrative>();
    public List<string> narrativesIds = new List<string>();
    public string introduction = "Εισαγωγή-Αφήγηση";
    public string entrance = "Το σώμα του φυματικού αναπαύεται σε μια λευκή χιονισμένη κορυφή. Το πνεύμα του απολαμβάνει το απόλυτο που έχει εισβάλει στη ζωή του: τον τέλειο 'ερωτα, την ιδανική γυναίκα, τη λύτρωση από τον πόνο. Αυτή είναι η ύλη της Εαρινής Συμφωνίας, ποιήματος ενός ζεύγος θνητών";
    public string room2N = "Νομίζω πως ο άνθρωπος που δεν τιμωρήθηκε ποτέ στη ζωή του, δεν ξέρει τι σημαίνει παραβίαση της απαγόρευσης. Και επειδή η ζωή είναι γεμάτη απογοητεύσεις, έμαθα να δουλεύω την ποίηση ξεπερνώντας 'τες";
    public string room3N = "Τις νύχτες αφουγκραζόμουν τους θρόους της σιγής... Κι ήρθες εσύ. Μη με καλέσεις ακόμη. Ας παρατείνουμε αυτές τις ώρες τις θαμπές τις υπερπληρωμένες που δυο κοσμοι ανταμώνονται που δυπ βαθιές φωνές ζυγιάζονται πάνω σε μια χορδή αργυρή και μια σταγόνα δρόσου σκιρτά και ταλαντεύεται στ'ανθος της νύχτας... Αγαπημένη τι προετοιμάζεται για εμάς μέσα στο βλέμμα των θεών πίσω από τη φωταψία;";
    public string corridor = "Γονατισμένος προσεύχομαι. θεέ μου θεέ μου η αγάπη μου΄'χε λείψει για να χαρώ και να νοήσω το μεγαλείο σου. Η αγάπη πιο μεγάλη από τη σιωπή γεφυρώνει το θεό με τον άνθρωπο και γεμίζει το απέραντο χάσμα με φτερά και λουλούδια. Κλείνω τα μάτια. Ζω και αγαπώ.";
    public string exitN = "Ένας γόος ευτυχίας ανεβαίνει απ΄τα σπλάχνα της γης, απ΄τα σπήλαια του δάσους μές στην έκθαμβη νύχτα διαπερνάει το χρόνο και το διάστημα. Μέσα του σφαδάζει όλη η ζωή και όλος ο θάνατος";
    public string room4N = "Ξεκρέμασε και πέταξε απ΄το ανοιχτό παράθυρο τις μελαγχολικές κορνίζες. Εσύ μου 'φερες τον καινούριο καιρό, το φως της αυγής και το αίμα μου. Να ο ήλιος που τρέχει μέσα στα δάση. δεν έχουμε αργήσει.";

    [ContextMenu("CreateXmlNarratives")]
    public void CreateXmlNarratives()
    {
        fileName = Path.Combine(Application.dataPath,"Narratives.xml");
        narratives.Clear();
        narratives.Add(new Narrative("Introduction", introduction));
        narratives.Add(new Narrative("Entrance", entrance));
        narratives.Add(new Narrative("Room2", room2N));
        narratives.Add(new Narrative("Room3", room3N));
        narratives.Add(new Narrative("Corridor", corridor));
        narratives.Add(new Narrative("Exit", exitN));
        narratives.Add(new Narrative("Room4", room4N));
        ListNarrative listNarrative = new ListNarrative();
        listNarrative.Narratives.Clear();
        listNarrative.Narratives = narratives;

        var settings = new XmlWriterSettings {
            Encoding = Encoding.UTF8,
            Indent = true
        };
        XmlSerializer serializer = new XmlSerializer(typeof(ListNarrative));
        using (var stream = XmlWriter.Create(fileName,settings))
        {
            serializer.Serialize(stream, listNarrative);
        }
    }

    [ContextMenu("CreateXmlNarrativesIds")]
    public void CreateXmlNarrativesIds()
    {
        fileName = Path.Combine(Application.dataPath, "NarrativesIds.xml");
        NarrativeIds _narrativeIds= new NarrativeIds();
        narrativesIds.Clear();
        narrativesIds.Add("WindowN");
        narrativesIds.Add("SaddnessN");
        narrativesIds.Add("Room2");
        narrativesIds.Add("MarbleLabelN");
        narrativesIds.Add("Introduction");
        narrativesIds.Add("GrammofwnoN");
        narrativesIds.Add("FeatherN");
        narrativesIds.Add("Exit");
        narrativesIds.Add("Entrance");
        narrativesIds.Add("CrossN");
        _narrativeIds.NarrativeIdsList = narrativesIds;
        var settings = new XmlWriterSettings
        {
            Encoding = Encoding.UTF8,
            Indent = true
        };
        XmlSerializer serializer = new XmlSerializer(typeof(NarrativeIds));
        using (var stream = XmlWriter.Create(fileName, settings))
        {
            serializer.Serialize(stream, _narrativeIds);
        }
    }
}

