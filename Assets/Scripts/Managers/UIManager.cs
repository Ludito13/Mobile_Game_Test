using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public TMP_Text coinsUI;
    public TMP_Text stamina;
    public TMP_Text staminaTimer;


    private void Awake()
    {
        instance = this;
    }

    public Image life;

    public void LifeBar(float percent)
    {
        life.fillAmount = percent;
    }

    public void Coins(int coins)
    {
        coinsUI.text = "Coins: " + coins.ToString();
    }

    public void Stamina(float currency, float maxCurrency)
    {
        stamina.text = currency.ToString() + "/" + maxCurrency.ToString();
    }

    public void StaminaTimer(string text)
    {
        staminaTimer.text = text;
    }
}
