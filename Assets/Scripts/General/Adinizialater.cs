using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Adinizialater : MonoBehaviour
{
    public static Adinizialater instance;

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOsGameId;
    [SerializeField] bool _testMode;
    private string _gameId;
    [SerializeField] Ads rewardAds;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);

        InitializedAds();

    }

    public void InitializedAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _iOsGameId : _androidGameId;
        Advertisement.Initialize(_gameId, this);
    }
}
