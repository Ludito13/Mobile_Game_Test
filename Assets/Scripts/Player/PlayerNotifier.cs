using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNotifier
{
    List<IObserver> _observers = new List<IObserver>();

    public PlayerNotifier(List<IObserver> observers) 
    {
        _observers = observers;
    }

   
}
