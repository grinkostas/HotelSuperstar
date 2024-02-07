
using System.Collections.Generic;
using System;
using UnityEngine;

public class SignalHub : MonoBehaviour
{
    private Dictionary<Type, ISignal> _signals = new Dictionary<Type, ISignal>();
    

    public T Get<T>() where T : ISignal
    {
        Type type = typeof(T);
        if (_signals.ContainsKey(type)) return (T)_signals[type];
        var signal = (T)Activator.CreateInstance(type);
        _signals.Add(type, signal);
        return signal;
    }
}
