using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCast : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _cell;
    [SerializeField] private Transform _aim;


    public GameObject GetCell() 
    {
        return _cell;
    }


 /*   private void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.CapsuleCast()(_aim.transform.position,_aim.transform.TransformDirection(Vector3.forward * 10 + Vector3.down * 5), out hit))
         //if(Physics.BoxCast(_aim.transform.position,))   
            if(hit.collider.TryGetComponent(out IImpact impact)) 
            {
                _cell = hit.collider.gameObject;
                _text.text = _cell.name;
                if (impact.TypeObj == TypeObjectImpact.PC) 
                {
                    _text.text = "PC";
                }
                if(impact.TypeObj == TypeObjectImpact.Enemy) 
                {
                    _text.text = "Enemy";
                }
                if (impact.TypeObj == TypeObjectImpact.Car)
                {
                    _text.text = "Car";
                }
            }
            else 
            {
                _text.text = "";
                _cell = null;
            }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IImpact impact))
        {
            _cell = other.gameObject;
            _text.text = _cell.name;
            if (impact.TypeObj == TypeObjectImpact.PC)
            {
                _text.text = "PC";
            }
            if (impact.TypeObj == TypeObjectImpact.Enemy)
            {
                _text.text = "Enemy";
            }
            if (impact.TypeObj == TypeObjectImpact.Car)
            {
                _text.text = "Car";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IImpact impact))
        {
            if (other.gameObject == _cell)
            {
                _text.text = "";
                _cell = null;

            }
        }
    }

  /*  private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out IImpact impact))
        {
            
        }  
    }*/
}
