using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class CarSignal : MonoBehaviour
{
    [SerializeField] private Light _light;
    public void Signal()
    {
        Debug.Log("Сигналю");
        StartCoroutine(CoroutineSignal());
    }

    private IEnumerator CoroutineSignal()
    {
        for (int i = 0; i < 10; i++)
        {
            _light.DOColor(Color.red, 0.1f);
            yield return new WaitForSeconds(0.1f);
            _light.DOColor(Color.white, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }

}
