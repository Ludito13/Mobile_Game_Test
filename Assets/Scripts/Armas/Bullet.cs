using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Todos los datos de la bala
public class Bullet : MonoBehaviour
{
    public float damage;
    public float maxSpeed;
    public float maxDistance;

    float _currentDistance;

    ObjectPool<Bullet> _objectPool;

    public void Inizialited(ObjectPool<Bullet> op)
    {
        _objectPool = op;
    }

    void Update()
    {
        BulletMovement();
    }

    public void BulletMovement()
    {
        transform.position += transform.forward * maxSpeed * Time.deltaTime;
        _currentDistance += maxSpeed * Time.deltaTime;

        if (_currentDistance >= maxDistance)
        {
            BulletSpawner.instance.ReturnObject(this);
            ResetBullet();
        }

    }


    public void ResetBullet()
    {
        _currentDistance = 0;
    }

    public static void TurnOn(Bullet b)
    {
        b.ResetBullet();
        b.gameObject.SetActive(true);
    }

    public static void TurnOff(Bullet b)
    {
        b.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var d = other.gameObject.GetComponent<IDamage>();

        if (d != null)
        {
            d.Damage(damage);
            BulletSpawner.instance.ReturnObject(this);
        }
    }
}
