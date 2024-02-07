using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour
{
    public void ExecuteWithDelay(Action action, float delay)
    {
        StartCoroutine(Delay(action, delay));
    }

    private IEnumerator Delay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }
}
