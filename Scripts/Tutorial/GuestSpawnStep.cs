using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class GuestSpawnStep : TutorialStep
{
    [Inject] private GuestSpawner _guestSpawner;
    protected override void OnEnter()
    {
        var guest = _guestSpawner.SpawnRandomGuest();
        guest.MakeEndlessPatience();
        NextStep();
    }
}
