using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen s_Instance;

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
    public void SetLoadingScreen(bool status)
    {
        this.gameObject.SetActive(status);
    }
}
