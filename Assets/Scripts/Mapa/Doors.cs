using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Desaparecen las puertas
public class Doors : MonoBehaviour
{
    void Start()
    {
        EventManager.Subscribe("Inactive Door", DisappearDoor);
    }

    public void DisappearDoor()
    {
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventManager.Unsuscribe("Inactive Door", DisappearDoor);
    }
}
