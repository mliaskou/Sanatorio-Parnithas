using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AudioManagerEnvironmentS
{
    [HideInInspector] public Dictionary<string,AudioClip> spatialSounds= new Dictionary<string, AudioClip>();
    List<string> _spatialSoundsNames = new() {"ParkOfSoulsS","SanatorioS","SoloS"};
    public IEnumerator Initialize()
    {

        foreach (string spatialSoundName in _spatialSoundsNames)
        {
            yield return AddressablesLoader.InstantiateGeneralAsync<AudioClip>(spatialSoundName, onComplete: (audioClip) => {
                spatialSounds.Add(spatialSoundName,audioClip);
            });
        }

        yield return null;
    }

    public IEnumerator DestroyFeature()
    {
        foreach (KeyValuePair<string, AudioClip> spatialSound in spatialSounds)
        {
            if (spatialSound.Value != null)
            {
                Debug.LogError("It is not null");
                UnityEngine.AddressableAssets.Addressables.Release(spatialSound.Value);
            }
        }

        spatialSounds.Clear();
        yield return null;
    }
}
