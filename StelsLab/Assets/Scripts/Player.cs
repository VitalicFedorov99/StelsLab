using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }


    public void Cast()
    {
        if (_state == StatePlayer.Stay)
        {
            _state = StatePlayer.Cast;
            _animPlayer.Cast();
            LogicCast();
        }
    }
    public void Attack()
    {
        if (_state == StatePlayer.Stay)
        {
            _state = StatePlayer.Attack;
            _animPlayer.Attack();
            LogicAttack();
        }
    }

    public void EndAction()
    {
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

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 10f)) 
        {
            Debug.LogError(hit.collider.name);
        }
    }

    private void LogicCast() 
    {
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, Vector3.forward);
    }
}


public enum StatePlayer
{
    Stay,
    Attack,
    Cast,
    Crouch
}