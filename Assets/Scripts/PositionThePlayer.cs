using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionThePlayer : MonoBehaviour
{

    public GameObject position1;
    public GameObject position2;
    public GameObject player;

    private void Awake()
    {
        if (!Menu.s_IsLoaded)
        {
            player.transform.position = position1.transform.position;
        }
        else
        {
            player.transform.position = position2.transform.position;
        }

    }
}
