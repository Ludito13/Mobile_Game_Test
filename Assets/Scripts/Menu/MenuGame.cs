using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Events;
//Se encarga del menú del juego (mientras se juega)
public class MenuGame : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private int coins;
    public Canvas[] canvas;
    public GameObject buttomPause;
    public AudioSource audios;
    public AudioStruct[] sounds;

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
        EventManager.Subscribe("Death", DeathScreen);
        EventManager.Subscribe("Win", WinScreen);

        audios = this.gameObject.GetComponent<AudioSource>();

    }

    private void OnDestroy()
    {
        EventManager.Unsuscribe("Death", DeathScreen);
        EventManager.Unsuscribe("Win", WinScreen);
    }

    public void DeathScreen()
    {
        canvas[2].enabled = true;
        Debug.Log("Perdi");

    }


    public void WinScreen()
    {
        canvas[3].enabled = true;
        Debug.Log("Gane");
    }

    public void PauseOn()
    {
        PlaySound(AudioStruct.Clips.Dialogo);
        Time.timeScale = 0f;
        canvas[0].enabled = true;

        buttomPause.SetActive(false);
    }

    public void PauseOff()
    {
        PlaySound(AudioStruct.Clips.Dialogo);
        Time.timeScale = 1f;
        canvas[0].enabled = false;
        buttomPause.SetActive(true);
    }

    public void StartConfirmation()
    {
        canvas[1].enabled = true;
        canvas[0].enabled = false;
        PlaySound(AudioStruct.Clips.Dialogo);
    }

    public void CancelConfirmation()
    {
        canvas[1].enabled = false;
        canvas[0].enabled = true;
        PlaySound(AudioStruct.Clips.Dialogo);
    }

    public void Options(Canvas c)
    {
        c.enabled = true;
        canvas[0].enabled = false;
    }

    public void QuitOption(Canvas c)
    {
        c.enabled = false;
        canvas[0].enabled = true;
    }


    public void Quit()
    {
        PlaySound(AudioStruct.Clips.Dialogo);
        Time.timeScale = 1f;
        CoinsManager.instance.AddCoins(coins);
        SceneManager.LoadScene("Menu");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        canvas[1].enabled = false;
        PlaySound(AudioStruct.Clips.Dialogo);
    }
}
