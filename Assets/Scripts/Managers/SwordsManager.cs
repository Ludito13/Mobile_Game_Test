using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SwordsManager : MonoBehaviour
{
    public static SwordsManager instance;

    [SerializeField] private int cost;

    public int swordsSelected;
    public List<Image> listnew = new List<Image>();
    public List<Image> soldOuts = new List<Image>();
    public AudioStruct[] sounds;
    public AudioSource audios;

    public Dictionary<AudioStruct.Clips, AudioClip> _sound = new Dictionary<AudioStruct.Clips, AudioClip>();

    private bool isBought = false;

    private void Awake()
    {
        Load();
        Debug.Log(swordsSelected);

        foreach (var s in sounds)
        {
            if (!_sound.ContainsKey(s.names))
                _sound.Add(s.names, s.aud);
        }

        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        audios = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlaySound(AudioStruct.Clips clip)
    {
        if (_sound.ContainsKey(clip))
            SoundManager.instance.PlaySound(_sound[clip]);
    }

    public void AddSword(Image s)
    {
        if (!listnew.Contains(s))
        {
            ShopManager.instance.GetImage(s);
            Save();
            isBought = true;
        }
    }

    public void AddSoldOut(Image s)
    {
        if (!soldOuts.Contains(s))
        {
            ShopManager.instance.GetSoldOut(s);
            Save();
        }
    }

    public void NextCharacter()
    {
        SaveManager.instance._data.listnew[swordsSelected].enabled = false;
        swordsSelected = (swordsSelected + 1) % SaveManager.instance._data.listnew.Count;
        SaveManager.instance._data.listnew[swordsSelected].enabled = true;
    }

    public void PreviousCharacter()
    {
        SaveManager.instance._data.listnew[swordsSelected].enabled = false;
        swordsSelected--;
        if (swordsSelected < 0)
            swordsSelected += listnew.Count;

        SaveManager.instance._data.listnew[swordsSelected].enabled = true;

    }

    private void OnDestroy()
    {
        Save();
    }

    public void StartGame()
    {
        PlaySound(AudioStruct.Clips.CompleteQuest);
        PlayerPrefs.SetInt("Selected Sword", swordsSelected);
        PlayerPrefs.SetInt("SwordColection", swordsSelected);
    }

    public void Save()
    {
        SaveManager.instance.Save();
        PlayerPrefs.SetInt("SwordColection", swordsSelected);
        PlayerPrefs.SetInt("Selected Sword", swordsSelected);
    }

    public void Load()
    {
       swordsSelected = PlayerPrefs.GetInt("Selected Sword", 0);
        PlayerPrefs.GetInt("SwordColection", 0);
    }
}
