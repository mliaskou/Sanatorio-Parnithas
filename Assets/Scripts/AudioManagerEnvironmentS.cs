using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerEnvironmentS
{
    List<AudioClip> spatialSounds;
    public IEnumerator Initialize(List<GameObject> environmentsSounds)
    {
        spatialSounds = new List<AudioClip>();

        foreach (GameObject go in environmentsSounds)
        {
            yield return AddressablesLoader.InstantiateGeneralAsync<AudioClip>(go.name, onComplete: (audioClip) => {
                go.GetComponent<Ghost_Sound>().Initialize(audioClip);
                spatialSounds.Add(audioClip);
            });
        }

        yield return null;
    }

    public IEnumerator DestroyFeature()
    {
        for (int i = spatialSounds.Count - 1; i >= 0; i++)
        {
            UnityEngine.Object.Destroy(spatialSounds[i]);
        }
        yield return null;
    }
}
