using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoneyDropStopper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out MoneyDropper moneyDropper))
            moneyDropper.StopDrop();
    }
}
