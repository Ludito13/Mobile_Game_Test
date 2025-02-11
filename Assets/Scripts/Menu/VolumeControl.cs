using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider[] sliders;
    public Image[] imagesMute;
    public AudioSource[] audioSources;
    public float generalVolume = 0.5f;
    public float musicVolume = 0.5f;

    void Start()
    {
        sliders[0].value = PlayerPrefs.GetFloat("volumenMusica", 0.5f);
        sliders[1].value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);

        for (int i = 0; i < audioSources.Length; i++)
        {
            for (int j = 0; j < sliders.Length; j++)
            {
                audioSources[i].volume = sliders[i].value;
            }
        }

        for (int i = 0; i < imagesMute.Length; i++)
        {
            imagesMute[i].enabled = true;
        }

        Mute();
    }

    public void ChangeMusicVolume(float v)
    {
        musicVolume = v;
        PlayerPrefs.SetFloat("volumenMusica", musicVolume);
        audioSources[0].volume = sliders[0].value;
        Mute();
    }

    public void ChangeGeneralVolume(float v)
    {
        generalVolume = v;
        PlayerPrefs.SetFloat("volumenAudio", generalVolume);
        audioSources[1].volume = sliders[1].value;
        Mute();
    }

    public void Mute()
    {
        if (musicVolume <= 0)
            imagesMute[0].enabled = true;
        else
            imagesMute[0].enabled = false;
        
        if(generalVolume <= 0)
            imagesMute[1].enabled = true;
        else
            imagesMute[1].enabled = false;
    }

    public void OnDestroy()
    {
        PlayerPrefs.SetFloat("volumenAudio", generalVolume);
        PlayerPrefs.SetFloat("volumenMusica", musicVolume);
    }
}
