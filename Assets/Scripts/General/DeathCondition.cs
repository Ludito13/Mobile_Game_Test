using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var p = other.GetComponent<IDeath>();

        if (p != null)
        {
            Debug.Log("Death Condition");
            p.Death();
        }
    }
}
