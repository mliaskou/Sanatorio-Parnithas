using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Ghost_Sound : MonoBehaviour
{
    private AudioSource audioSource;
    public void Initialize()
    {
        audioSource = this.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Play();
        }     
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Stop();
        }
    }
}
