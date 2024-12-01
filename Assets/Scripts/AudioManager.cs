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
    public IEnumerator Initialize()
    {
        _narrativesAudioSource = GetComponent<AudioSource>();
        AudioClip[] narratives = Resources.LoadAll<AudioClip>("Narratives/");
        foreach(AudioClip audio in narratives)
        {
            if (audio != null)
            {
                _audioNarratives.Add(audio.name, new Audio(audio, false));
            }
            else
            {
                Debug.LogError($"Audio {audio.name} is null");
            }
        }
        _AudioManagerEnvironmentS = new AudioManagerEnvironmentS();
        yield return _AudioManagerEnvironmentS.Initialize(_EnvironmentsSounds);
        yield return null;
    }

    public void Play(string audioName)
    {
        if (_audioNarratives.ContainsKey(audioName))
        {
            Debug.Log("exists");
            _narrativesAudioSource.clip = _audioNarratives[audioName]._AudioClip;
            if (!_audioNarratives[audioName]._AudioHasPlayed)
            {
                _audioNarratives[audioName]._AudioHasPlayed = true;
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
            Debug.Log($"The audio with name {audioName} does not exist");
        }   
    }

    public bool HasAudio(string audioName)
    {
        bool result = false;
        if (_audioNarratives.ContainsKey(audioName))
        {
            result = true;
        }
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
        foreach(var value in _audioNarratives.Values)
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
        yield return _AudioManagerEnvironmentS.DestroyFeature();
        _AudioManagerEnvironmentS = null;
    }
}
