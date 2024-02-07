using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sunbed : ActionPlace
{
    protected override void OnRemoveGuest(Guest guest)
    {
        guest.MoveExit();
    }
}
