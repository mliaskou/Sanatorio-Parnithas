using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;
    public GameObject player;

        public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
        player = GameObject.Find("player");
        player.GetComponent<FirstPersonController>().enabled = false;
        
        
        }
        public void Resume()
        {
        player = GameObject.Find("player");
        player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.visible = false;
        isPaused = false;
           
        }
        public void Menu()
        {
            SceneManager.LoadScene("Menu");
        }

         public void Quit()
        {
            UnityEditor.EditorApplication.isPlaying = false;
         }

    public void ShowMenu()
    {
        pauseMenuUI.SetActive(true);
    }

    public void HideMenu()
    {
        pauseMenuUI.SetActive(false);
    }

}
