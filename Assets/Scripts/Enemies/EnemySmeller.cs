using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enemigo que huele si el player está sangrando, si lo esta y esta cerca de su rango el smeller lo seguirá
public class EnemySmeller : Enemies
{
    public override void Start()
    {
        base.Start();
        FindObjectOfType<Player>().Subscribe(this);

    }

    public override void Damage(float damage)
    {
        base.Damage(damage);
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Movement()
    {
        base.Movement();
    }

    public override void Notify(bool n)
    {
        base.Movement();
        base.Notify(n);
    }

    public new void OnDestroy()
    {
        FindObjectOfType<Player>().Unsubscribe(this);
    }
}
