using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Recompensas (en este caso monedas) que se activan cuando tocan al jugador
public class Reward : MonoBehaviour
{
    [SerializeField]
    private int coinsReward = 10;
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

    private void Start()
    {
        audios = this.gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        IPlayers p = other.gameObject.GetComponent<IPlayers>();

        if(p != null)
        {
            CoinsManager.instance.AddCoins(coinsReward);
            PlaySound(AudioStruct.Clips.ObtainCoin);
            this.gameObject.SetActive(false);
        }
    }
}
