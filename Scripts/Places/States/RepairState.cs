using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RepairState : PlaceState
{
    [SerializeField] private ContinuousRequest _repairRequest;
    
    public override void OnEnterPhase()
    {
        _repairRequest.gameObject.SetActive(true);
        _repairRequest.MakeRequest();
        _repairRequest.Done += OnRequestDone;
    }

    public override void OnExitPhase()
    {
        
        _repairRequest.Done -= OnRequestDone;
        _repairRequest.gameObject.SetActive(false);
    }

    private void OnRequestDone(Request request)
    {
        ExitPhase();
    }
}
