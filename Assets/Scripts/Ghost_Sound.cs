using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ghost_Sound : MonoBehaviour
{
    private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource != null)
            {
                GetAndPlayAudioClip();
            }
            else
            {
                Debug.LogError("Audio clip is null, get audiosource and audioclip");
                audioSource = GetComponent<AudioSource>();
                GetAndPlayAudioClip();               
            }
        }
    }

    private void GetAndPlayAudioClip() {

        if (audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.clip = GameStateManager._Instance._AudioManager._AudioManagerEnvironmentS.spatialSounds[gameObject.name];
            audioSource.Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource.GetComponent<AudioSource>().clip != null)
            {
                audioSource.Stop();
            }
            else
            {
                Debug.LogError("Audio clip is null");
            }
        }        
    }
}
