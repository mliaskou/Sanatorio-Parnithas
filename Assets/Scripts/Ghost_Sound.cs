using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Ghost_Sound : MonoBehaviour
{
    private AsyncOperationHandle<AudioClip> audioClipHandle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameStateManager._Instance._EnvironmentAudioSource != null)
            {
                GetAndPlayAudioClip();
            }
        }
    }

    private void GetAndPlayAudioClip()
    {
        GameStateManager._Instance.ReleaseEnvironmentAudioSource();
        StartCoroutine(AddressablesLoader.InstantiateGeneralAsync<AudioClip>(gameObject.name,
        onDone: (audioClip, handle) =>
        {
            GameStateManager._Instance.SetEnvironmentAudioSource(audioClip, handle);
        }));
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameStateManager._Instance.ReleaseEnvironmentAudioSource();
        }
    }
}
