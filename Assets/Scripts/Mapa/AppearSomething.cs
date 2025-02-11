using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Evento para que aparezcan las puertas
public class AppearSomething : MonoBehaviour
{
    void Start()
    {
        EventManager.Subscribe("Active Door", AppearDoor);
    }

    public void AppearDoor()
    {
        this.gameObject.SetActive(true);
    }
}
