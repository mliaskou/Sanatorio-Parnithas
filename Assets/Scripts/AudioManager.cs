using System;
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
    public Action<int> _ShowAndIncreaseCountText;

    [HideInInspector] public AudioManagerEnvironmentS _AudioManagerEnvironmentS;
    [SerializeField] public List<GameObject> _EnvironmentsSounds = new List<GameObject>();
    List<string> _narrativeIds = new List<string>();

    private AudioSource _narrativesAudioSource;
    private Dictionary<string, Audio> _audioNarratives = new Dictionary<string, Audio>();
    int count = 0;
    public IEnumerator Initialize()
    {
         gameObject.AddComponent<AudioSource>();
        _narrativesAudioSource = gameObject.AddComponent<AudioSource>();
        _AudioManagerEnvironmentS = new AudioManagerEnvironmentS();

        yield return GameStateManager._Instance._UIManager._SaveXml.DeserializeXml<NarrativeIds>("NarrativesIds", (narrativeIdsXml) => {
            _narrativeIds = narrativeIdsXml.NarrativeIdsList;
        });
        foreach (string id in _narrativeIds)
        {
            if (!string.IsNullOrEmpty(id))
            {
                yield return AddressablesLoader.InstantiateGeneralAsync<AudioClip>(id, (audio) =>
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
            _narrativesAudioSource.clip = _audioNarratives[audioID]._AudioClip;
            if (!_audioNarratives[audioID]._AudioHasPlayed)
            {                
                _audioNarratives[audioID]._AudioHasPlayed = true;
                count++;
                _ShowAndIncreaseCountText?.Invoke(count);
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
