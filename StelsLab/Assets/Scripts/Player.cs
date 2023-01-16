using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private bool _isCrouch = false;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotate;

    [SerializeField] private float _speedCrouch;
    [SerializeField] private float _speedStay;
    [SerializeField] private StatePlayer _state = StatePlayer.Stay;



    
    [SerializeField] private Image _imageHealth;
    [SerializeField] private Image _imageComouflage;


    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _coefComouflage;

    [SerializeField] private float _maxCoefComouflage;
    [SerializeField] private GameObject _cell;

    [SerializeField] private PlayerCast _playerCast;
    private AnimatorPlayer _animPlayer;
  

    public void Crouch()
    {
        _isCrouch = !_isCrouch;
        if (_isCrouch)
        {
            _state = StatePlayer.Crouch;
            _coefComouflage -= 0.25f;
            _imageComouflage.fillAmount = _coefComouflage / _maxCoefComouflage;
        }
        else
        {
            _state = StatePlayer.Stay;
            _coefComouflage += 0.25f;
            _imageComouflage.fillAmount = _coefComouflage / _maxCoefComouflage;
        }
        _animPlayer.Crouch(_isCrouch);
    }


    public void Damage() 
    {
        _currentHealth--;
        _imageHealth.fillAmount = _currentHealth / _maxHealth;
    }


    public void AddCoef(float coef) 
    {
        _coefComouflage += coef;
        _imageComouflage.fillAmount = _coefComouflage / _maxCoefComouflage;
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

        if (_state == StatePlayer.Stay|| _state == StatePlayer.Crouch)
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
        _cell = _playerCast.GetCell();
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
        if (_state == StatePlayer.Attack)
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
        _currentHealth = _maxHealth;
        _imageHealth.fillAmount = _currentHealth / _maxHealth;
        _imageComouflage.fillAmount = _coefComouflage / _maxCoefComouflage;
    }


    
    private void LogicAttack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 5f))
        {
            Debug.LogError(hit.collider.name);
            if (hit.collider.TryGetComponent(out CarSignal car))
            {
                car.Signal();
            }
        }
    }





    private void LogicCast()
    {
        if (_cell != null)
        {
            if (_cell.TryGetComponent(out IImpact impact))
            {
                impact.Impact();
            }

        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 5f);
    }
}


public enum StatePlayer
{
    Stay,
    Attack,
    Cast,
    Crouch
}