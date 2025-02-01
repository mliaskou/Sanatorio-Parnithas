using UnityEngine;


public class SoundPlay : MonoBehaviour
{
    public void ShowNarrativeCanvas(string name)
    {
        GameStateManager._Instance._UIManager._ShowNarrativeCanvas?.Invoke(this.gameObject,name);
    }

    public void PlayAudio()
    {
        if (GameStateManager._Instance._AudioManager.HasAudio(this.name))
        {
            GameStateManager._Instance._AudioManager.Play(this.name);
        }
        
    }
    public void StopAudio()
    {
        if (GameStateManager._Instance._AudioManager.HasAudio(this.name))
        {
            GameStateManager._Instance._AudioManager.Stop(this.name);
        }
    }
}
