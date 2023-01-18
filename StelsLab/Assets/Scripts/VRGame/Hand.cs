using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IImpact impact))
        {
            Debug.Log("че то делаю");
            impact.Impact();
        }

       /* if (other.TryGetComponent(out Laser laser))
        {
            laser.Impact();
        }
        if(other.TryGetComponent(out CarSignal carSignal))
        {
               
        }*/
    }
}
