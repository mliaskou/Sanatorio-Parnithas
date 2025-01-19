using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ghost_Sound : MonoBehaviour
{
    private AudioSource audioSource;
    public void Initialize(AudioClip audio)
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audio;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (audioSource.GetComponent<AudioSource>().clip != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Audio clip is null");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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
