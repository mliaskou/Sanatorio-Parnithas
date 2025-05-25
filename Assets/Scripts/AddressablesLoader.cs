using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using System;

public static class AddressablesLoader
{
    public static IEnumerator InstantiateGeneralAsync<T>(string key, Action<T> onComplete, Action<T, AsyncOperationHandle<T>> onDone=null) where T:class
    {
        AsyncOperationHandle<T> opHandle;
        opHandle = Addressables.LoadAssetAsync<T>(key);
        yield return opHandle;

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("succeedeed");
            T obj = opHandle.Result;
            onComplete?.Invoke(obj);
            onDone?.Invoke(obj, opHandle);
        }        
    }

    public static IEnumerator InstantiateGameObject(string key, Action<GameObject> onComplete)
    {
        AsyncOperationHandle<GameObject> opHandle;
        opHandle = Addressables.InstantiateAsync(key);
        yield return opHandle;
    
        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("succeedeed");
            GameObject obj = opHandle.Result;
            onComplete?.Invoke(obj);
        }
    }

    public static void InstantiateSyncGameObject(string key, Action<GameObject> onComplete)
    {
        var op = Addressables.LoadAssetAsync<GameObject>(key);
        GameObject go = op.WaitForCompletion();
        onComplete?.Invoke(go);
    }
}
