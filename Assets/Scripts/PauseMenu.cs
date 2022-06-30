using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameStateManager gsm;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameStateManager.isPaused)
            {
                gsm.Resume();
                gsm.HideMenu();
               
            }
            else
            {
                gsm.Pause();
                gsm.ShowMenu();
            }
        }
    }
}
