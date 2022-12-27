using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animEnemy;
    private RobotEnemy _robot;

    private string ATTACK = "Attack";
    private string MOVE = "Walk";
    private string RUN = "Run";

    private void Start()
    {
        _animEnemy = GetComponent<Animator>();
        _robot = GetComponent<RobotEnemy>();
    }

    public void Attack()
    {
        _animEnemy.SetTrigger(ATTACK);
    }

    public void Run(bool flag)
    {
        _animEnemy.SetBool(RUN, flag);
    }

    public void Move(bool flag)
    {
        _animEnemy.SetBool(MOVE, flag);
    }

    public void EndAction()
    {
        _robot.EndAction();
        Debug.Log("Выключился");
    }
}
