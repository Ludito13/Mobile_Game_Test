using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtomTranslate : MonoBehaviour
{
    public string ID;
    public TMP_Text text;

    private void Start()
    {
        Translate();
        LocalizationManager.instance.enventTranslate += Translate;
    }

    public void Translate()
    {
        text.text = LocalizationManager.instance.GetTranslate(ID);
    }
}
