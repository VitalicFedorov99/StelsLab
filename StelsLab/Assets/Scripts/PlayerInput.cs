using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private Player _player;
    Collider m_Collider;
    RaycastHit m_Hit;
    bool m_HitDetect;
    float m_MaxDistance=20f;

    //private AnimatorPlayer _animPlayer;

    private void Start()
    {
        _player = GetComponent<Player>();
        m_Collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            _player.Crouch();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) 
        {
            _player.Attack();
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            _player.Cast();
        }
        _player.Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
       
    }

    void FixedUpdate()
    {
        
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
        if (m_HitDetect)
        {
          
            Debug.Log("Hit : " + m_Hit.collider.name);
        }
        

    }

    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

      
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
       /* else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }*/
    }
}
