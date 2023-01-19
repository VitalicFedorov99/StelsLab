using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EnergyPlayer : MonoBehaviour
{
    public float Energy;
    public float MaxEnergy;

    public float Health;

    public float StartHealth;


    public Hand Right;
    public Hand Left;


    public Image _imageHealth;

    public Image _imageEnergy;

    public void Start()
    {
        Health = StartHealth;
        _imageHealth.fillAmount = Health / StartHealth;
        _imageEnergy.fillAmount = Energy / MaxEnergy;
    }

    public void Damage(float damage)
    {
        Health -= damage;
        _imageHealth.fillAmount = Health / StartHealth;
        if (Health < 0)
        {
            SceneManager.LoadScene("SceneVR");
        }
    }

    

    public void UpdateEnergy(float energy)
    {
        Energy += energy;
        if (Energy > MaxEnergy)
        {
            Energy = MaxEnergy;
        }
        _imageEnergy.fillAmount = Energy / MaxEnergy;
    }


    public void Dead()
    {

    }
}
