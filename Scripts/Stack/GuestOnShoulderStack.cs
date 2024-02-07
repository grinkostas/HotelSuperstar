using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GuestOnShoulderStack : MonoBehaviour
{
    [SerializeField] private Transform _stackPoint;
    [SerializeField] private Vector3 _delay;
    
    private List<CreativeGuest> _guests = new List<CreativeGuest>();

    public List<CreativeGuest> Guests => _guests;

    public void Add(CreativeGuest guest)
    {
        guest.transform.SetParent(_stackPoint);
        guest.DisableCollider();
        guest.transform.position = _stackPoint.position + _delay * _guests.Count;
        guest.transform.localRotation = Quaternion.identity;
        guest.Movement.Agent.enabled = false;
        _guests.Add(guest);
    }

    public CreativeGuest TakeLast()
    {
        if (_guests.Count == 0)
            return null;
        var guest = _guests.Last();
        _guests.Remove(guest);
        guest.Movement.Agent.enabled = true;
        return guest;
    }
}
