using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerEnvironmentS
{
    AudioClip[] spatialSounds;
    public IEnumerator Initialize(List<GameObject> environmentsSounds)
    {
         spatialSounds = Resources.LoadAll<AudioClip>("SpatialSounds/");
        foreach (AudioClip audio in spatialSounds)
        {
           foreach(GameObject go in environmentsSounds)
            {
                if(go.name == audio.name){
                    go.GetComponent<AudioSource>().clip = audio;
                    go.GetComponent<Ghost_Sound>().Initialize();
                }
            }
        }
        yield return null;
    }

    public IEnumerator DestroyFeature()
    {
        for(int i=spatialSounds.Length-1; i>=0; i++)
        {
            UnityEngine.Object.Destroy(spatialSounds[i]);
        }
        yield return null;
    }
}
