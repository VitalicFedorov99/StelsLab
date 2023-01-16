using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;
public class RobotEnemy : MonoBehaviour, IImpact
{

    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private EnemyState _enemyState;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _timeWait;
    [SerializeField] private float _timeStan;
    [SerializeField] private float __coolDown;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRun;

    [SerializeField] private float _anger;
    [SerializeField] private float _maxAnger;
    [SerializeField] private Image _image;

    

    [SerializeField] private Sprite _stanImage;
    [SerializeField] private Sprite _ImageAnger;


    private bool _isObserv = false;
    private EnemyAnimator _animEnemy;
    private int currentId = 0;
    private NavMeshAgent _agent;
    private Tween _twenn;

    public TypeObjectImpact TypeObj => TypeObjectImpact.Enemy;

    public void EndAction()
    {
        if (Vector3.Distance(transform.position, _target.transform.position) < 2f)
        {
            Attack();
        }
        else
        {
            _enemyState = EnemyState.Patrol;
        }
    }

    public void Stan()
    {
        StartCoroutine(CoroutuneWait(_timeStan));
    }

    public void Impact()
    {
        StartCoroutine(CoroutuneWait(_timeStan));
    }


    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animEnemy = GetComponent<EnemyAnimator>();
    }


    private void Update()
    {
        if (_enemyState == EnemyState.Patrol)
            Patrol();

        if (_enemyState == EnemyState.Run)
            Run();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if(!_isObserv)
        if(Vector3.Distance(transform.position, _player.transform.position) < 5f)
        {
            StartCoroutine(CoroutineObserve());
        }
    }

    private void Patrol()
    {
        _agent.enabled = true;
        _agent.speed = _speed;
        _agent.SetDestination(_patrolPoints[currentId].position);
        transform.LookAt(_patrolPoints[currentId].position);
        _animEnemy.Move(true);
        _animEnemy.Run(false);
        if (Vector3.Distance(transform.position, _patrolPoints[currentId].position) < 1.5f)
        {
            currentId++;
            if (currentId >= _patrolPoints.Length)
            {
                currentId = 0;
            }
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (_target == null)
            if (other.TryGetComponent(out Player player))
            {
                _image.DOColor(Color.red, 1f);
            }
    }

    private void Run()
    {
        _agent.speed = _speedRun;
        _agent.enabled = true;
        _agent.SetDestination(_target.transform.position);
        transform.LookAt(_target.transform.position);
        _animEnemy.Run(true);
        _animEnemy.Move(false);
        if (Vector3.Distance(transform.position, _target.transform.position) > 7)
        {
            _twenn = _image.DOColor(Color.white, 4);
            StartCoroutine(CoroutuneWait(_timeWait));
        }

        if (Vector3.Distance(transform.position, _target.transform.position) < 2f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _enemyState = EnemyState.Attack;
        _agent.enabled = false;
        _animEnemy.Attack();
        _animEnemy.Move(false);
        _animEnemy.Run(false);
    }


    private IEnumerator CoroutuneWait(float time)
    {
        _enemyState = EnemyState.Wait;
        _animEnemy.Run(false);
        _animEnemy.Move(false);
        yield return new WaitForSeconds(time);
        _enemyState = EnemyState.Patrol;
        _target = null;
    }

    private IEnumerator CoroutineCoolDown()
    {
        yield return new WaitForSeconds(1);
        if (Vector3.Distance(transform.position, _target.transform.position) < 2f)
        {
            Attack();
        }
        else
        {
            _enemyState = EnemyState.Patrol;
        }
    }

    private IEnumerator CoroutineObserve()
    {
        _isObserv = true;
        yield return new WaitForSeconds(2);
        if (Vector3.Distance(transform.position, _player.transform.position) < 5f)
        {
            Debug.LogError("Я с агрился");
            _target = _player.gameObject;
            _enemyState = EnemyState.Run;
        }
        _isObserv = false;
    }

   
}


public enum EnemyState
{
    Patrol,
    Idle,
    Attack,
    Run,
    Wait
}
