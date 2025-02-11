using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enemigo base
public class EnemyBase : Enemies
{
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

    public override void BaseAttack()
    {
        base.BaseAttack();
    }
}
