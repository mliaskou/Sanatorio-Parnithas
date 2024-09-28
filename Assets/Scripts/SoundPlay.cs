using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundPlay : MonoBehaviour
{
    public AudioSource source;
    public int count;
    public bool _audioHasPlayed = false;

    public void SoundList()
    {
        source.Play();
        if (!_audioHasPlayed)
        {
            SoundsArray s = GameObject.FindObjectOfType<SoundsArray>();
            s.Array[count - 1] = true;
            s.ArraySound(count - 1);
            if (s.AreNarrativesCompleted())
            {
                Debug.Log("You have completed all the narratives");
            }
            _audioHasPlayed = true;
        }
    }

    public void StopAudio()
    {
        source.Stop();
        Debug.LogError("stop audio1");
    }

}
