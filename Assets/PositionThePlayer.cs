using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionThePlayer : MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    public GameObject player;
    // Start is called before the first frame update

    void Awake()
    {
        if (Menu.isLoaded1 == true)
        {
            player.transform.position = position1.transform.position;
        }
        if (Menu.isLoaded2 == true)
        {
            player.transform.position = position2.transform.position;
        }

    }
   

}
