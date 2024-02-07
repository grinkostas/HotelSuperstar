using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class LoadingOperation : MonoBehaviour
{
    public UnityAction End;

    public abstract void Load();

    protected void Finish()
    {
        End?.Invoke();
    }

}
