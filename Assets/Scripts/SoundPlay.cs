using UnityEngine;


public class SoundPlay : MonoBehaviour
{
    public int count;

    public void ShowNarrativeCanvas(string name)
    {
        UIManager._Instance._ShowNarrativeCanvas?.Invoke(this.gameObject,name);
    }

    public void PlayAudio()
    {    
        if (GameStateManager._Instance._AudioManager.HasAudio(this.gameObject.name))
        {

            GameStateManager._Instance._AudioManager.Play(this.gameObject.name);
        }
        
    }
    public void StopAudio()
    {
        if (GameStateManager._Instance._AudioManager.HasAudio(this.gameObject.name))
        {
            GameStateManager._Instance._AudioManager.Stop(this.gameObject.name);
        }
    }
}
