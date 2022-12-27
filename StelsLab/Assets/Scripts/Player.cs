using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player : MonoBehaviour
{
    [SerializeField] private bool _isCrouch = false;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;
    [SerializeField] private StatePlayer _state = StatePlayer.Stay;

    private AnimatorPlayer _animPlayer;
    private Rigidbody _rg;

    public void Crouch()
    {
        _isCrouch = !_isCrouch;
        if (_isCrouch)
        {
            _state = StatePlayer.Crouch;
        }
        else
        {
            _state = StatePlayer.Stay;
        }
        _animPlayer.Crouch(_isCrouch);
    }

   



    public void Move(float vertical, float horizontale)
    {
        float translation = vertical * _speed;
        float rotation = horizontale * _speedRotate;
        bool speedFlag = false;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        if (Mathf.Abs(vertical) > 0.4f)
        {
            speedFlag = true;
        }

        _animPlayer.Move(speedFlag);

        //_meshAgent.SetDestination(new Vector3(0, 0, translation));
        if (_state == StatePlayer.Stay)
        {
            transform.Translate(0, 0, translation);
            transform.Rotate(0, rotation, 0);
        }
    }


    public void Cast()
    {
        if (_state == StatePlayer.Stay)
        {
            _state = StatePlayer.Cast;
            _animPlayer.Cast();
        }
    }
    public void Attack()
    {
        if (_state == StatePlayer.Stay)
        {
            _state = StatePlayer.Attack;
            _animPlayer.Attack();
        }
    }

    public void EndAction()
    {
        if(_state == StatePlayer.Attack)
        {
            LogicAttack();
        }
        if (_state == StatePlayer.Cast)
        {
            LogicCast();
        }
        _state = StatePlayer.Stay;
    }
    private void Start()
    {
        _animPlayer = GetComponent<AnimatorPlayer>();
        _rg = GetComponent<Rigidbody>();
    }

    private void LogicAttack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f)) 
        {
            Debug.LogError(hit.collider.name);
            if(hit.collider.TryGetComponent(out CarSignal car))
            {
                car.Signal();
            }
        }
    }

    private void LogicCast() 
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.TransformDirection(Vector3.forward)*5f);
    }
}


public enum StatePlayer
{
    Stay,
    Attack,
    Cast,
    Crouch
}