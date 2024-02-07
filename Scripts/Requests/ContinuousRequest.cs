using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Zenject;

public class ContinuousRequest : Request, IProgressible
{
    [SerializeField] private UpgradeSelect _duration;
    [Inject] private UpgradesController _upgradesController;
    public UnityAction<float> ProgressChanged { get; set; }
    
    protected override void OnUseItem()
    {
        DisableInteract();
        StartCoroutine(CompletingRequest());
    }

    private IEnumerator CompletingRequest()
    {
        float wastedTime = 0;
        float duration = _upgradesController.GetValue(_duration);
        while (wastedTime <= duration)
        {
            wastedTime += Time.deltaTime;
            ProgressChanged?.Invoke(wastedTime/duration);
            yield return null;
        }
        SignalHub.Get<Signals.RequestCompetedSignal>().Dispatch(this);
        ProgressChanged?.Invoke(0);
        Complete();
    }

    protected override void OnRequest()
    {
        SignalHub.Get<Signals.NewCleaningRequest>().Dispatch(this);
    }
    
    

}
