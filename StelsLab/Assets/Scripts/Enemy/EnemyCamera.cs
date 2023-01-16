using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemyCamera : MonoBehaviour,IImpact
{

    public TypeObjectImpact TypeObj => TypeObjectImpact.Enemy;
    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private Transform[] _movementPoints;
    [SerializeField] private float _timeStan;
     private Collider _coll;

    private int currentId=0;

   // private NavMeshAgent _agent;

  

    public void Impact()
    {
        StartCoroutine(CoroutineStan());
    }
    private void Start()
    {
        _speed = _maxSpeed;
        _coll = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player player)) 
        {
            Debug.Log("Попался");
            player.Damage();
        }
    }

    private void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, _movementPoints[currentId].position, Time.deltaTime * _speed);
        if (Vector3.Distance(transform.position, _movementPoints[currentId].position) < 1.5f)
        {
            currentId++;
            if (currentId >= _movementPoints.Length)
            {
                currentId = 0;
            }
        }
    }


    private IEnumerator CoroutineStan() 
    {
        _speed = 0;
        _coll.enabled = false;
        yield return new WaitForSeconds(_timeStan);
        _speed = _maxSpeed;
        _coll.enabled = true;
    }
}
