using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWave : MonoBehaviour
{
    void Update()
    {
        CountEnemies();
    }

    public void CountEnemies()
    {
        if (this.transform.childCount < 1)
        {
            EventManager.Trigger("Inactive Door");
        }
    }
}
