using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{

    public static AppManager s_Instance;

    public GameObject PositionThePlayer;
    void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(this.gameObject);
            return;//Avoid doing anything else
        }
        s_Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
