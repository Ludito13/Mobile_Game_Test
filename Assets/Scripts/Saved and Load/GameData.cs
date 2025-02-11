using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class GameData
{
    public List<Image> listnew = new List<Image>();
    public List<Image> soldOut = new List<Image>();
    public bool isBought;
}
