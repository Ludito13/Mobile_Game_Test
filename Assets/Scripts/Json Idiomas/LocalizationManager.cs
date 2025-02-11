using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;
    public DataLocalization[] textAssets;
    public SystemLanguage language;
    public event Action enventTranslate;

    Dictionary<SystemLanguage, Dictionary<string, string>> _translate = new Dictionary<SystemLanguage, Dictionary<string, string>>();



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _translate = LanguageU.LoadTranslate(textAssets);
        }
        else
            Destroy(gameObject);
    }

    public void BTN_ChangeLanguages(float lang)
    {
        switch (lang)
        {
            case 0:
                ChangeLang(SystemLanguage.Spanish);
                break;
            case 1:
                ChangeLang(SystemLanguage.English);
                break;
        }
    }

    public void ChangeLang(SystemLanguage lang)
    {
        if (language != lang)
        {
            language = lang;
            enventTranslate?.Invoke();
        }

    }

    public string GetTranslate(string id)
    {
        if (!_translate.ContainsKey(language))
            return "No lang";

        if (!_translate[language].ContainsKey(id))
            return "No Id";


        return _translate[language][id];
    } 
}
