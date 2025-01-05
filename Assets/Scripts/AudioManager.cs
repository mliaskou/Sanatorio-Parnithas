using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio
{
    public AudioClip _AudioClip;
    public bool _AudioHasPlayed;

    public Audio(AudioClip audioClip, bool audioHasPlayed)
    {
        _AudioClip = audioClip;
        _AudioHasPlayed = audioHasPlayed;
    }
}
public class AudioManager : MonoBehaviour
{
    private AudioSource _narrativesAudioSource;
    private Dictionary<string, Audio> _audioNarratives = new Dictionary<string, Audio>();
    int count = 0;

    [HideInInspector] public AudioManagerEnvironmentS _AudioManagerEnvironmentS;
    [SerializeField] public List<GameObject> _EnvironmentsSounds = new List<GameObject>();
    List<string> _narrativeIds = new List<string>() {
    "WindowN","SaddnessN","Room2","MarbleLabelN","Introduction","GrammofwnoN","FeatherN","Exit","Entrance","CrossN"
    };
    public IEnumerator Initialize()
    {
        _narrativesAudioSource = GetComponent<AudioSource>();
        _AudioManagerEnvironmentS = new AudioManagerEnvironmentS();
        foreach (string id in _narrativeIds)
        {
            if (!string.IsNullOrEmpty(id))
            {
                yield return AddressablesLoader.InstantiateGameObject(id, (audio) =>
                {

                    _audioNarratives.Add(id, new Audio(audio, false));
                });
            }
            else
            {
                Debug.LogError($"Audio {id} is null");
            }
        }
        
        yield return _AudioManagerEnvironmentS.Initialize(_EnvironmentsSounds);
        yield return null;
    }

    public void Play(string audioID)
    {
        if (_audioNarratives.ContainsKey(audioID))
        {
            Debug.Log("exists");
            _narrativesAudioSource.clip = _audioNarratives[audioID]._AudioClip;
            if (!_audioNarratives[audioID]._AudioHasPlayed)
            {
                _audioNarratives[audioID]._AudioHasPlayed = true;
                count++;
                UIManager._Instance._NarrativesInventory._CountText.text = count.ToString();
            }
            if (AreNarrativesCompleted())
            {
                Debug.LogError("All narratives have been completed");
            }
            _narrativesAudioSource.Play();
        }
        else
        {
            Debug.Log($"The audio with name {audioID} does not exist");
        }
    }

    public bool HasAudio(string audioName)
    {
        bool result = false;
        if (_audioNarratives.ContainsKey(audioName))
        {
            result = true;
        }
        Debug.LogError(result);
        return result;
    }
    public void Stop(string audioName)
    {
        if (_audioNarratives.ContainsKey(audioName))
        {
            _narrativesAudioSource.Stop();
            _narrativesAudioSource.clip = null;
        }
        else
        {
            Debug.Log($"The audio with name {audioName} does not exist");
        }
    }

    public bool AreNarrativesCompleted()
    {
        bool result = false;
        foreach (var value in _audioNarratives.Values)
        {
            if (value._AudioHasPlayed)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        return result;
    }

    public IEnumerator DestroyFeature()
    {
        _audioNarratives.Clear();
        foreach (Audio audio in _audioNarratives.Values)
        {
            UnityEngine.AddressableAssets.Addressables.Release(audio);
        }
        yield return _AudioManagerEnvironmentS.DestroyFeature();
        _AudioManagerEnvironmentS = null;
    }
}
