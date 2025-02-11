using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StaminaGame : MonoBehaviour
{
    public static StaminaGame instace;
    public int currentEnergy;
    public int maxEnergy;
    public int restoreDuration;

    [SerializeField] Text energyText;
    [SerializeField] Text timerText;

    private DateTime _nextEnergyTime;
    private DateTime _lastEnergyTime;
    private bool _isRestoring = false;

    public void Awake()
    {
        if (instace == null)
            instace = this;
        else
            Destroy(this.gameObject);

        UIManager.instance.Stamina(currentEnergy, maxEnergy);

    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("currentEnergy"))
        {
            PlayerPrefs.SetInt("currentEnergy", maxEnergy);
            Load();
            StartCoroutine(RestoreEnergy());
            UpdateEnergy();
        }
        else
        {
            Load();
            StartCoroutine(RestoreEnergy());
        }
    }

    public void UseEnergy(int amount)
    {
        if (currentEnergy >= 1)
        {
            currentEnergy -= amount;
            UpdateEnergy();

            if (!_isRestoring)
            {
                if (currentEnergy + 1 == maxEnergy)
                    _nextEnergyTime = AddDuration(DateTime.UtcNow, restoreDuration);

                StartCoroutine(RestoreEnergy());
            }
        }
        else
        {
            Debug.Log("Energia Insuficiente");
        }
    }

    IEnumerator RestoreEnergy()
    {
        UpdateEnergyTimer();
        _isRestoring = true;

        while (currentEnergy < maxEnergy)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            DateTime nextDateTime = _nextEnergyTime;
            bool isEnergyAdding = false;

            while (currentDateTime > nextDateTime)
            {
                if (currentEnergy < maxEnergy)
                {
                    isEnergyAdding = true;
                    currentEnergy++;
                    UpdateEnergy();
                    DateTime timeToAdd = _lastEnergyTime > nextDateTime ? _lastEnergyTime : nextDateTime;
                    nextDateTime = AddDuration(timeToAdd, restoreDuration);
                }
                else
                    break;
            }

            if (isEnergyAdding)
            {
                _lastEnergyTime = DateTime.UtcNow;
                _nextEnergyTime = nextDateTime;
            }

            UpdateEnergyTimer();
            UpdateEnergy();
            Save();
            yield return null;
        }

        _isRestoring = false;
    }

    public void AddEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += 10;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

            UpdateEnergy();
        }
        else
        {
            UpdateEnergy();
        }
    }

    DateTime AddDuration(DateTime dateTime, int duration)
    {
        return dateTime.AddSeconds(duration); 
    }

    void UpdateEnergyTimer()
    {
        if (currentEnergy >= maxEnergy)
        {
            UIManager.instance.StaminaTimer("Full");
            NotificationsApp.instance.CreateNotification(0.03f);
            return;
        }

        TimeSpan time = _nextEnergyTime - DateTime.UtcNow;
        string timeValue = String.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
        UIManager.instance.StaminaTimer(timeValue);
    }

    void UpdateEnergy()
    {
        UIManager.instance.Stamina(currentEnergy, maxEnergy);
    }

    DateTime stringToDate(string dateTime)
    {
        if (String.IsNullOrEmpty(dateTime))
            return DateTime.UtcNow;
        else
            return DateTime.Parse(dateTime);

    }

    void Load()
    {
        currentEnergy = PlayerPrefs.GetInt("currentEnergy");
        _nextEnergyTime = stringToDate(PlayerPrefs.GetString("_nextEnergyTime"));
        _lastEnergyTime = stringToDate(PlayerPrefs.GetString("_lastEnergyTime"));
    }

    void Save()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
        PlayerPrefs.SetString("_nextEnergyTime", _nextEnergyTime.ToString());
        PlayerPrefs.SetString("_lastEnergyTime", _lastEnergyTime.ToString());
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("currentEnergy", currentEnergy);
    }
}
