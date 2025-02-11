using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//Se encarga del menú principal del juego.
public class MenuManager : MonoBehaviour
{
    public Animator anim;
    public Canvas optionCanvas;
    public Canvas generalCanvas;
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
    }

    public void PlaySound(AudioStruct.Clips clip)
    {
        if (_sound.ContainsKey(clip))
            SoundManager.instance.PlaySound(_sound[clip]);
    }

    void Start()
    {
        audios = this.gameObject.GetComponent<AudioSource>();
        //DesactiveCanvas(optionCanvas);
    }

    //public void ActiveCanvas(Canvas first)
    //{
    //    PlaySound(AudioStruct.Clips.Dialogo);
    //    first.enabled = true;
    //    generalCanvas.enabled = false;
    //}

    //public void DesactiveCanvas(Canvas first)
    //{
    //    PlaySound(AudioStruct.Clips.Dialogo);
    //    first.enabled = false;
    //    generalCanvas.enabled = true;
    //}

    public void GameCanvas(Canvas first)
    {
        first.enabled = false;
    }

    public void Lel(string actions)
    {
        anim.SetTrigger(actions);
    }

    //public void Levels()
    //{
    //    PlaySound(AudioStruct.Clips.Dialogo);
    //    anim.SetTrigger("GoLevels");
    //}

    //public void GoBack()
    //{
    //    PlaySound(AudioStruct.Clips.Dialogo);
    //    anim.SetTrigger("GoBackFromLevels");
    //}

    //public void GoShop()
    //{
    //    PlaySound(AudioStruct.Clips.Dialogo);
    //    anim.SetTrigger("GoShop");
    //}

    //public void GoBackMain()
    //{
    //    PlaySound(AudioStruct.Clips.Dialogo);
    //    anim.SetTrigger("GoBackFromShop");
    //}

    public void Levels(string l)
    {
        SceneManager.LoadScene(l);

    }

    //public void FirstLevel()
    //{
    //    PlaySound(AudioStruct.Clips.SelectLevel);
    //    SceneManager.LoadScene("Level 1");
    //}

    //public void SecondLevel()
    //{
    //    PlaySound(AudioStruct.Clips.SelectLevel);
    //    SceneManager.LoadScene("Level 2");
    //}

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
