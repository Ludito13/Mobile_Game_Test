using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct AudioStruct
{
    public AudioClip aud;

    public Clips names;

    public enum Clips
    {
        Bala,
        CompleteQuest,
        Compra, 
        PlayerDamage,
        Dash,
        GolpeDeEspada,
        GolpeEnemigo,
        IdleEnemigo,
        ObtainCoin, 
        SelectLevel,
        Dialogo
    }
}
