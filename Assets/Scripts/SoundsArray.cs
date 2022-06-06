using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SoundsArray : MonoBehaviour
{
    public GameStateManager gsm;
    public GameObject player;
    public GameObject position3;
    public bool[] Array = new bool[9] { false, false, false, false, false, false, false, false, false };
    private bool isAllTrue = false;

    public GameObject Image;

    // Update is called once per frame

    public void Start()
    {
        isAllTrue = true;
       
    }
    void Update()
    {

        foreach (bool b in Array)
        {
            if (b)
            {
                isAllTrue = true;
            }
            else
            {
                isAllTrue = false;
                break;
            }
        }
        if (isAllTrue == true)
        {
          
          
        }
   
    }

    public void ReturnToGame()
    {
        gsm.Resume();
        Image.SetActive(false);
    }

    public void ArraySound()
    {
        for (int i = 0; i < Array.Length; i++)
        {
            print("Scene " + (i + 1) + ": " + Array[i]);
           

        }
      
    }
   
    
}

