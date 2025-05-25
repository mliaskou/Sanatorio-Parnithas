using UnityEngine;


public class SoundPlay : MonoBehaviour
{
    public void ShowNarrativeCanvas(string name)
    {
        GameStateManager._Instance._UIManager._ShowNarrativeCanvas?.Invoke(gameObject,name);
    }

    public void PlayAudio()
    {
        if (GameStateManager._Instance._AudioManager.HasAudio(name))
        {
            GameStateManager._Instance._AudioManager.Play(name);
        }
        
    }
    public void StopAudio()
    {
        if (GameStateManager._Instance._AudioManager.HasAudio(name))
        {
            GameStateManager._Instance._AudioManager.Stop(name);
        }
    }
}
