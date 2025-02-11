using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartDialogue : MonoBehaviour, IStartDialogue
{
    public GameObject dialogue;

    public void Active()
    {
        dialogue.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
