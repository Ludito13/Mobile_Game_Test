using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Hijo del EnemyWeapons, arma del huge enemy
public class HugeEnemyWeapon : Weapons
{
    public override void ActiveCol()
    {
        base.ActiveCol();
    }

    public override void InactiveCol()
    {
        base.InactiveCol();
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }
}
