using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Menu : MonoBehaviour
{
    public static Menu Instance;
    public static bool isLoaded1;
    public static bool isLoaded2;


    void Awake()
    {

        // if the singleton hasn't been initialized yet
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    public void Sanatorio()
    {
        Debug.Log("IsTrue1");
        SceneManager.LoadScene("SampleScene");
        isLoaded1 = true;
        isLoaded2 = false;
       
    }
    public void ParkofSouls()
    {
        Debug.Log("IsTrue2");
        SceneManager.LoadScene("SampleScene");
        isLoaded2 = true;
        isLoaded1 = false;
    }

    public void ApplicationQuit()
    {
        Application.Quit();

    }

  
}




