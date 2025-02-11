using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Instanciar bala
public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner instance;

    public Bullet prefab;
    public int bulletsCount;
    ObjectPool<Bullet> _objectPool;

    public void Awake()
    {
        instance = this;
    }

    public ObjectPool<Bullet> ObjectPool
    {
        get { return _objectPool; }
    }

    void Start()
    {
        _objectPool = new ObjectPool<Bullet>(Factory, Bullet.TurnOn, Bullet.TurnOff, bulletsCount);
    }

    public void ShootBullet()
    {
        var o = _objectPool.GetObject();
        o.transform.position = transform.position;
        o.transform.forward = transform.forward;
    }

    public void ReturnObject(Bullet b)
    {
        _objectPool.ReturnObject(b);
    }

    Bullet Factory()
    {
        var b = Instantiate(prefab);
        b.Inizialited(_objectPool);
        return b;
    }
}
