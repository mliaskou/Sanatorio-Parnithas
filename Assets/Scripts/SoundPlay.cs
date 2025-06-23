using UnityEngine;


public class SoundPlay : MonoBehaviour
{
    public void ShowNarrativeCanvas(string name)
    {
        GameStateManager._Instance._UIManager._ShowNarrativeCanvas?.Invoke(gameObject,name);
    }

    public void PlayAudio()
    {
        GameStateManager._Instance._AudioManager.Play(name); 
    }
    
    public void StopAudio()
    {
        GameStateManager._Instance._AudioManager.Stop(name);
    }
}
