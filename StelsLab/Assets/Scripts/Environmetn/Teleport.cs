using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _place;
    [SerializeField] private bool _isWork;
    [SerializeField] private ParticleSystem _teleportVfx;


    private Collider _coll;


    private void Start()
    {
        _coll = GetComponent<Collider>();
    }


    public void Work()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player)) 
        {
            player.transform.position = _place.transform.position;
        }
    }
}
