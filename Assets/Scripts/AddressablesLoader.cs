using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;
using System;

public static class AddressablesLoader
{
    public static IEnumerator InstantiateGameObject(string key, Action<AudioClip> onComplete)
    {
        AsyncOperationHandle<AudioClip> opHandle;
        opHandle = Addressables.LoadAssetAsync<AudioClip>(key);
        yield return opHandle;

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("succeedeed");
            AudioClip obj = opHandle.Result;
            onComplete?.Invoke(obj);
        }
        
    }
}
