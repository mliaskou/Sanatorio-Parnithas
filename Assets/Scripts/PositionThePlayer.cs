using UnityEngine;

public class PositionThePlayer:MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    public static bool S_IsLoaded;
    public void InitializePlayerPosition(GameObject player)
    {
        if (S_IsLoaded)
        {
            player.transform.position = position1.transform.position;
        }
        else
        {
            player.transform.position = position2.transform.position;
        }
    }
}
