using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DispenserManager : MonoBehaviour
{
    [SerializeField] private List<Dispenser> _dispensers;

    public Dispenser GetItemDispenser(Item item)
    {
        if (item == null)
            return null;
        return _dispensers.Find(x => x.Id == item.ItemMakerId);
    }
}
