using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool<T>
{
    public delegate T FactoryMethod();
    int _initialStock;

    List<T> _currentStock = new List<T>();
    FactoryMethod _factoryMethod;
    Action<T> _turnOnCallBack;
    Action<T> _turnOffCallBack;


    public ObjectPool(FactoryMethod f, Action<T> tOn, Action<T> tOff, int iStock)
    {
        _factoryMethod = f;
        _turnOnCallBack = tOn;
        _turnOffCallBack = tOff;

        for (int i = 0; i < iStock; i++)
        {
            var o = _factoryMethod();
            _turnOffCallBack(o);
            _currentStock.Add(o);
        }
    }
 
    public T GetObject()
    {
        T result;

        if (_currentStock.Count > 0)
        {
            result = _currentStock[0];
            _currentStock.RemoveAt(0);
        }
        else
            result = _factoryMethod();

        _turnOnCallBack(result);

        return result;
    }

    public void ReturnObject(T o)
    {
        _turnOffCallBack(o);
        _currentStock.Add(o);
    }
}
