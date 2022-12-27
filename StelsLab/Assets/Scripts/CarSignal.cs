using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSignal : MonoBehaviour
{
    [SerializeField] private Light _light;
    public void Signal()
    {
        Debug.Log("Сигналю");
    }
}
