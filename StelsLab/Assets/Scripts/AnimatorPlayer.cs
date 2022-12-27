using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayer : MonoBehaviour
{
    private Animator _animator;
    private Player _player;

    private string CROUCH = "Crouch";
    private string MOVE = "Move";
    private string ATTACK = "Attack";
    private string CAST = "Cast";



    public void Crouch(bool flag)
    {
        _animator.SetBool(CROUCH, flag);
    }

    public void Move(bool flag)
    {
        _animator.SetBool(MOVE, flag);
    }

    public void Attack()
    {
        _animator.SetTrigger(ATTACK);
    }

    public void Cast()
    {
        _animator.SetTrigger(CAST);
    }

    public void EndFight()
    {
        _player.EndAction();
    }
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }
}
