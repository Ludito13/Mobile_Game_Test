using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Esta clase se encarga de cargar la espada seleccionada
public class LoadSword : MonoBehaviour
{
    public static LoadSword instance;

    public List<GameObject> _swordsSelections = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else DontDestroyOnLoad(this); 

        int selectedSword = PlayerPrefs.GetInt("Selected Sword");
        GameObject sword = _swordsSelections[selectedSword];
        sword.SetActive(true);
    }

    private void OnDestroy()
    {
        int selectedSword = PlayerPrefs.GetInt("Selected Sword");
        GameObject sword = _swordsSelections[selectedSword];

    }
}
