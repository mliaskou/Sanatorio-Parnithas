using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameStateManager gsm;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GameStateManager.isPaused)
            {
                gsm.Resume();
               
            }
            else
            {
                gsm.Pause();
                gsm.ShowMenu();
            }
        }
    }
}
