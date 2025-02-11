using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCondition : MonoBehaviour
{
    public int killsToAdvance;
    int _counterKills;

    private void Start()
    {
        Debug.Log("Min kills = " + killsToAdvance);
        EventManager.Subscribe("Counter Wave", NewKill);
    }

    public void NewKill()
    {
        _counterKills++;

        if (_counterKills >= killsToAdvance)
        {
            EventManager.Unsuscribe("Counter Wave", NewKill);
            EventManager.Trigger("Inactive Door");
        }
    }
}
