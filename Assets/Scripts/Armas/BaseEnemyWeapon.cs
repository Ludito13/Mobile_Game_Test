using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyWeapon : Weapons
{
    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    public override void ActiveCol()
    {
        base.ActiveCol();
    }

    public override void InactiveCol()
    {
        base.InactiveCol();
    }
}
