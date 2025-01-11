using UnityEngine;

public class LoadingScreen
{
    GameObject _loadingScreen;
   public LoadingScreen(GameObject loadingscreen)
    {
        _loadingScreen = loadingscreen;
    }
    public void SetLoadingScreen(bool status)
    {
        _loadingScreen.SetActive(status);
    }

    public void DestroyFeature()
    {
        UnityEngine.AddressableAssets.Addressables.Release(_loadingScreen);
    }
}
