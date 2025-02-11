using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons : MonoBehaviour
{
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected Collider col;

    private void Start()
    {
        col = this.gameObject.GetComponent<Collider>();
        col.enabled = false;
    }


    public virtual void ActiveCol()
    {
        col.enabled = true;
    }

    public virtual void InactiveCol()
    {
        col.enabled = false;
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        var d = other.gameObject.GetComponent<IDamage>();

        if (d != null)
            d.Damage(damage);
    }
}
