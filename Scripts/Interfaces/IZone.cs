
using UnityEngine;
using UnityEngine.Events;

public interface IZone
{
    public UnityAction Interacted { get; set; }
    public GameObject GameObject { get; }
}
