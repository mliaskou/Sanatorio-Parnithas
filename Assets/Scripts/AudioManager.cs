using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Audio
{
    public bool _AudioHasPlayed;
    public AudioClip _AudioClip;
    public AsyncOperationHandle<AudioClip> _AudioHandle;

    public Audio(bool audioHasPlayed, AudioClip clip)
    {
        _AudioHasPlayed = audioHasPlayed;
        _AudioClip = clip;
    }
}
public class AudioManager
{
    public Action<int> _ShowAndIncreaseCountText;
    List<string> _narrativeIds = new List<string>();

    private AudioSource _narrativesAudioSource;
    private Dictionary<string, Audio> _audioNarratives = new Dictionary<string, Audio>();
    private string _currentAudioId="";
    int count = 0;

    public IEnumerator Initialize()
    {
        _narrativesAudioSource = GameStateManager._Instance._NarrativeAudioSource;

        yield return GameStateManager._Instance._UIManager._SaveXml.DeserializeXml<NarrativeIds>("NarrativesIds", (narrativeIdsXml) => {
            _narrativeIds = narrativeIdsXml.NarrativeIdsList;
        });

        foreach (string id in _narrativeIds)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _audioNarratives.Add(id, new Audio(false, null));
            }
            else
            {
                Debug.LogError($"AudioID {id} is null");
            }
        }
    }

    public void Play(string audioID)
    {
        if (HasAudio(audioID) && _currentAudioId.Equals(audioID))
        {
            _narrativesAudioSource.clip = _audioNarratives[audioID]._AudioClip;
            _narrativesAudioSource.Play();
            Debug.LogError("AudioId has the same id as the name");
            return;
        }

        if (_audioNarratives.ContainsKey(audioID))
        {
            ResetAudioPlayer();
            IEnumerator enumerator = AddressablesLoader.InstantiateGeneralAsync<AudioClip>(audioID,
             onDone: (audioClip, audioHandle) =>
             {
                 _currentAudioId = audioID;
                 _audioNarratives[audioID]._AudioClip = audioClip;
                 _audioNarratives[audioID]._AudioHandle = audioHandle;
                 _narrativesAudioSource.clip = _audioNarratives[audioID]._AudioClip;
                 _narrativesAudioSource.Play();
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
             });
             GameStateManager._Instance.StartCoroutineOverwrite(enumerator);
        }
        else
        {
            Debug.Log($"The audio with name {audioID} does not exist");
        }

    }

    private void ResetAudioPlayer()
    {
        if (!string.IsNullOrEmpty(_currentAudioId))
        {
            if (_audioNarratives.ContainsKey(_currentAudioId))
            {
                UnityEngine.AddressableAssets.Addressables.Release(_audioNarratives[_currentAudioId]._AudioHandle);
                _audioNarratives[_currentAudioId]._AudioClip = null;
                _narrativesAudioSource.clip=null;
                _currentAudioId = "";
            }
        }
    }

    private bool HasAudio(string audioId)
    {
        bool result = false;
        if (_audioNarratives.ContainsKey(audioId))
        {
            if (_audioNarratives[audioId]._AudioClip != null)
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
    public void Stop(string audioName)
    {
        if (_audioNarratives.ContainsKey(audioName))
        {
            _narrativesAudioSource.Stop();
        }
        else
        {
            Debug.Log($"The audio with name {audioName} does not exist");
        }
    }

    private bool AreNarrativesCompleted()
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
        _ShowAndIncreaseCountText=null;
        _narrativesAudioSource.clip=null;
        _audioNarratives.Clear();
        _currentAudioId="";
        yield return null;
    }

    public void DisableAudioSource(){
        _narrativesAudioSource.Stop();
        _narrativesAudioSource.clip = null;
    }
}
