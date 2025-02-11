using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Actualiza las monedas
public class CoinsManager : MonoBehaviour
{
    public static CoinsManager instance;

    public int coins;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        coins = PlayerPrefs.GetInt("Current Coins");
        UIManager.instance.Coins(coins);
    }

    public void AddCoins(int add)
    {
        coins += add;
        PlayerPrefs.SetInt("Current Coins", coins);
        Refresh();
    }

    public void RestCoins(int subs)
    {
        coins -= subs;
        PlayerPrefs.SetInt("Current Coins", coins);
        Refresh();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("Current Coins", coins);
    }

}
