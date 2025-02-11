using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Dialogues : MonoBehaviour, IPointerClickHandler
{
    public Image panel;
    public Image dynamic;
    public GameObject alpha;
    public Canvas generalCanvas;
    public float typingMaxTime;
    public TextMeshProUGUI dialoguesText;
    public string ID;
    public TextMeshProUGUI nameText;
    public AudioSource audios;
    [SerializeField, TextArea(4, 6)] string[] dialogues;

    private bool isDialogueStart;
    private int lineIndex;

    private void Start()
    {
        nameText.text = name;
        DialogueActive();
        audios = this.gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        Debug.Log(lineIndex);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DialogueActive();
    }

    public void DialogueActive()
    {
        if (!isDialogueStart)
            StartDialogue();
        else if (dialoguesText.text == LocalizationManager.instance.GetTranslate(ID))
            NextDialogue();
        else
        {
            StopCoroutine(Line());
            dialoguesText.text = LocalizationManager.instance.GetTranslate(ID);
        }
    }

    public void StartDialogue()
    {
        isDialogueStart = true;
        panel.enabled = true;
        dynamic.enabled = false;
        generalCanvas.enabled = false;
        lineIndex = 0;
        StartCoroutine(Line());
    }

    public void NextDialogue()
    {
        lineIndex++;
        if (lineIndex > LocalizationManager.instance.GetTranslate(ID).Length)
        {
            StartCoroutine(Line());
        }
        else
        {
            StopDialogue();
        }
    }

    public void StopDialogue()
    {
        isDialogueStart = false;
        dynamic.enabled = true;
        panel.enabled = false;
        alpha.SetActive(false);
        generalCanvas.enabled = true;
    }

    public IEnumerator Line()
    {
        dialoguesText.text = string.Empty;

        if(isDialogueStart)
        {
            foreach (char ch in LocalizationManager.instance.GetTranslate(ID))
            {
                dialoguesText.text += ch;
                yield return new WaitForSeconds(typingMaxTime);
            }      
        }
    }
}
