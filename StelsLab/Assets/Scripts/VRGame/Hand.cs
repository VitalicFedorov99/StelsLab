using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public EnergyPlayer energyPlayer;
    public float damage;
    public bool IsEnergy=false;

    public GameObject Energy;




    public void OnHand()
    {

        if (energyPlayer.Energy == energyPlayer.MaxEnergy)
        {
            Energy.SetActive(true);
            IsEnergy = true;
        }
    }

    public void Test()
    {
        Debug.Log("Test");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IImpact impact))
        {
            Debug.Log("че то делаю");
            impact.Impact();
        }

        if(other.TryGetComponent(out CollectEnergy energy))
        {
            energyPlayer.UpdateEnergy(energy.Energy);
            Destroy(energy.gameObject);
        }

        
        if (IsEnergy)
        {
            if (other.TryGetComponent(out RobotEnemy robot))
            {
                Debug.LogError("Ѕью по нему");
                robot.Damage(damage);
            }
        }
    }



}
