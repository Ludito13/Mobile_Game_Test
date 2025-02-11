using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioStruct[] sounds;
    public AudioSource aud;

    [SerializeField] private SaveManager _data;

    Dictionary<AudioStruct.Clips, AudioClip> _sound = new Dictionary<AudioStruct.Clips, AudioClip>();

    private void Awake()
    {
        foreach (var s in sounds)
        {
            if (!_sound.ContainsKey(s.names))
                _sound.Add(s.names, s.aud);
        }

        if (instance == null)
            instance = this;
        else Destroy(this.gameObject);
    }

    public void PlaySound(AudioStruct.Clips clip)
    {
        if (_sound.ContainsKey(clip))
            SoundManager.instance.PlaySound(_sound[clip]);
    }

    public void BTN_Sounds(string soundsName)
    {
        switch (soundsName)
        {
            case "Coins":
                PlaySound(AudioStruct.Clips.Compra);
                break;
            case "Dialogue":
                PlaySound(AudioStruct.Clips.Dialogo);
                break;
            case "Level":
                PlaySound(AudioStruct.Clips.SelectLevel);
                break;
        }
    }

    public void Load()
    {
        _data.Save();
    }

    public void Save()
    {
        _data.Load();
    }
}
