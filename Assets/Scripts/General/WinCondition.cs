using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public int minKillToWin;
    int _counterKills;

    private void Start()
    {
        EventManager.Subscribe("Kill Counter", NewKill);
        Debug.Log(minKillToWin);
    }

    public void NewKill()
    {
        _counterKills++;

        if(_counterKills >= minKillToWin)
        {
            EventManager.Unsuscribe("Kill Counter", NewKill);
            EventManager.Trigger("Win");
        }
    }
}
