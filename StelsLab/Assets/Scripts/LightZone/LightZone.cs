using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightZone : MonoBehaviour
{
    [SerializeField] private float _coef;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player)) 
        {
            player.AddCoef(_coef);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.AddCoef(-_coef);
        }
    }
}
