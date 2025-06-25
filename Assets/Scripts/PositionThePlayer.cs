using System;
using System.Collections;
using UnityEngine;

public class PositionThePlayer
{
    private GameObject _player;
    private Action _resume;

    private Transform _sanatoriumTransform;
    private Transform _ParkOfSoulsTransform;

    private Transform _parkOfSoulsParent;
    private Transform _sanatoriumParent;

    public PositionThePlayer(GameObject player, Action resume, Transform parkOfSoulsParent, Transform sanatoriumParent)
    {
        _player = player;
        _resume = resume;
        _parkOfSoulsParent=parkOfSoulsParent;
        _sanatoriumParent =sanatoriumParent;
    }
    public IEnumerator InitializePlayerPosition(bool isLoaded)
    {
        if (isLoaded)
        {
            if (_sanatoriumTransform == null)
            {
                yield return AddressablesLoader.InstantiateGameObject("sanatoriumPosition", (gameObject) =>
                {
                    _sanatoriumTransform = gameObject.transform;
                    _sanatoriumTransform.SetParent(_sanatoriumParent, false);
                    _player.transform.position = _sanatoriumTransform.position;
                    _resume?.Invoke();
                });
            }else{
                    _player.transform.position = _sanatoriumTransform.position;
                    _resume?.Invoke();
            }
        }
        else
        {
            if (_ParkOfSoulsTransform == null)
            {
                yield return AddressablesLoader.InstantiateGameObject("parkOfSoulsPosition", (gameObject) =>
                              {
                                  _ParkOfSoulsTransform = gameObject.transform;
                                  _ParkOfSoulsTransform.SetParent(_parkOfSoulsParent, false);
                                  _player.transform.position = _ParkOfSoulsTransform.position;
                                  _resume?.Invoke();
                              });

            }else{
                _player.transform.position = _ParkOfSoulsTransform.position;
                _resume?.Invoke();
            }
        }
        yield return null;
    }

    private void OnDestroy()
    {
        _resume = null;
    }
}
