using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static Ads instace;

    [SerializeField] Button _showAdButton;
    [SerializeField] string _androidID = "Interstitial_Android";
    [SerializeField] string _iOsID = "Interstitial_iOS";
    [SerializeField] int _coins = 10;
    string _adUnitId;

    private void Awake()
    {
        if (instace == null)
            instace = this;
        else Destroy(gameObject);

        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOsID : _androidID;
    }

    public void LoadAds()
    {
        Advertisement.Load(_adUnitId);
    }

    public void ShowAds()
    {
        Advertisement.Show(_adUnitId);
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {

        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            CoinsManager.instance.AddCoins(_coins);

            Advertisement.Load(_adUnitId, this);
        }
    }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }
}
