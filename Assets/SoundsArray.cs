using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class SoundsArray : MonoBehaviour
{

    // Start is called before the first frame update

    public GameObject txt;
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

        /*if (Array[0] == true && Array[1] == true && Array[2]==true && Array[3] ==true && Array[4]== true && Array[5]==true && Array[6]==true && Array[7]==true && Array[8]== true)
        {
            isAllTrue = true;
        }*/

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
            txt.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                gsm.Pause();
                Image.SetActive(true);
            }
        }
        
            
        
        /* if(PlayerController.count >=9 )
         {

             if(Input.GetKeyDown(KeyCode.Return))
             {
                 Text.SetActive(true);
                 gsm.Pause();
             }

         }*/

   
    }


    public void LastNar()
    {
        gsm.Resume();
        player.transform.position = position3.transform.position;
        Image.SetActive(false);
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

