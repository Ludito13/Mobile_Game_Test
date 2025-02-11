using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//cura la vida del player al tocarlo
public class CurePotion : MonoBehaviour
{
    public float maxHealth;


    private void OnTriggerEnter(Collider other)
    {
        var c = other.gameObject.GetComponent<IPlayers>();

        if (c != null)
        {
            if(c.Health(maxHealth))
                Destroy(this.gameObject);
        }
    }
}
