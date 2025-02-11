using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Manager de Eventos
public class EventManager
{
    public static Dictionary<string, Action> _events = new Dictionary<string, Action>();
    
    public static void Subscribe(string name, Action method)
    {
        if (_events.ContainsKey(name))
            _events[name] += method;
        else
            _events.Add(name, method);
    }

    public static void Unsuscribe(string name, Action method)
    {
        if (_events.ContainsKey(name))
            _events[name] -= method;

        if (_events[name] == null)
            _events.Remove(name);
    }

    public static void Trigger(string name)
    {
        if (_events.ContainsKey(name))
            _events[name]();
    }
}
