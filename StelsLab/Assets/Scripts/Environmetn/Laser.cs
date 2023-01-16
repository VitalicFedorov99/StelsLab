using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IImpact
{
    [SerializeField] private Light _light;
    [SerializeField] private GameObject _laser;
    [SerializeField] private bool _isWork;

    public TypeObjectImpact TypeObj => TypeObjectImpact.PC;


    public void Impact()
    {
        Work();
    }

    public void Work()
    {
        _isWork = !_isWork;
        if (_isWork)
        {
            _laser.SetActive(_isWork);
            _light.color = Color.red;
        }
        else
        {
            _laser.SetActive(_isWork);
            _light.color = Color.green;
        }
    }
}
