
using UnityEngine;

public class Treadmill : ActionPlace
{
    protected override void OnPlaceGuest(Guest guest)
    {
        guest.HideAmmo();
        guest.Movement.ForceWalk();
    }

    protected override void OnRemoveGuest(Guest guest)
    {
        guest.ShowAmmo();
        guest.MoveExit();
    }
}
