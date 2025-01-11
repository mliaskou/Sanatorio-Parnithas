using System;
using System.Collections;
using UnityEngine;

public class PositionThePlayer:MonoBehaviour
{
    public GameObject position1;
    public GameObject position2;
    private GameObject _player;
    private Action _resume;

    public void SetDepedencies(GameObject player, Action resume)
    {
        _player = player;
        _resume = resume;
    }
    public IEnumerator InitializePlayerPosition(bool isLoaded)
    {
        if (isLoaded)
        {
            _player.transform.position = position1.transform.position;
            _resume?.Invoke();
        }
        else
        {
            _player.transform.position = position2.transform.position;
            _resume?.Invoke();
        }
        yield return null;
    }

    private void OnDestroy()
    {
        _resume = null;
    }
}
