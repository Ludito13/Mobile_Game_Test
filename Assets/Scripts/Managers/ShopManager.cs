using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
//Manager encargado de la tienda
public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [SerializeField] private int coins;
    [SerializeField] private int maxCoins;
    [SerializeField] private TMP_Text coinsUI;


    private Image swordIm;
    private Image soldOutIm;
    private bool isBought;
    public AudioStruct[] sounds;
    public AudioSource audios;

    public Dictionary<AudioStruct.Clips, AudioClip> _sound = new Dictionary<AudioStruct.Clips, AudioClip>();

    private void Awake()
    {

        foreach (var s in sounds)
        {
            if (!_sound.ContainsKey(s.names))
                _sound.Add(s.names, s.aud);
        }
        
        if (instance != null) Destroy(this);
        else instance = this;
    }

    public void PlaySound(AudioStruct.Clips clip)
    {
        if (_sound.ContainsKey(clip))
            SoundManager.instance.PlaySound(_sound[clip]);
    }

    private void Start()
    {
        audios = this.gameObject.GetComponent<AudioSource>();

        ActiveSoldOut();
    }

    public void ActiveSoldOut()
    {
        for (int i = 0; i < SaveManager.instance._data.soldOut.Count; i++)
        {
            SaveManager.instance._data.soldOut[i].enabled = true;          
        }
    }

    public void GetImage(Image s)
    {
        swordIm = s;
    }

    public void GetSoldOut(Image s)
    {
        soldOutIm = s;
    }

    public void AddCoins(int add)
    {
        CoinsManager.instance.AddCoins(add);
        PlaySound(AudioStruct.Clips.Compra);
    }

    public void BuyThing(int cost)
    {
        if (CoinsManager.instance.coins >= cost && !SaveManager.instance._data.listnew.Contains(swordIm))
        {
            PlaySound(AudioStruct.Clips.Compra);
            CoinsManager.instance.RestCoins(cost);
            SaveManager.instance._data.listnew.Add(swordIm);
            SaveManager.instance._data.soldOut.Add(soldOutIm);
        }
        coins = Mathf.Clamp(coins, 0, maxCoins);
    }

    private void OnDestroy()
    {
        SaveManager.instance.Save();
    }

    public void SoldOut(Image sO)
    {

        SaveManager.instance._data.isBought = true;
        ActiveSoldOut();
        

    }
}
