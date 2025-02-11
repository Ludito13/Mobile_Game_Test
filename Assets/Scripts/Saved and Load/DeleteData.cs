using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
//Elimina los datos guardados
public class DeleteData : MonoBehaviour
{
    public void Delete()
    {
        PlayerPrefs.DeleteAll();

        for (int i = 0; i < SaveManager.instance._data.soldOut.Count; i++)
        {
            SaveManager.instance._data.soldOut[i].enabled = false;                
        }

        SaveManager.instance._data.listnew.RemoveRange(1, SaveManager.instance._data.listnew.Count - 1);

        //SaveManager.instance._data.listnew.Clear();
        SaveManager.instance._data.soldOut.Clear();
    }

}
