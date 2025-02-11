using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWave : MonoBehaviour
{
    void Update()
    {
        CountEnemies();
    }

    public void CountEnemies()
    {
        if (this.transform.childCount < 1)
        {
            EventManager.Trigger("Second Wave");
        }
    }
}
