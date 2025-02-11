using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Enemigo que dispara
public class EnemyShooter : Enemies
{
    public override void Damage(float damage)
    {
        base.Damage(damage);
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Rotation()
    {
        base.Rotation();
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void InstanceBullet()
    {
        base.InstanceBullet();
    }
}
