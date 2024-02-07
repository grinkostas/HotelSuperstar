using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.Events;
using Zenject;

public class LimitedInTimeRequest : Request, IProgressible
{
    [SerializeField] private UpgradeSelect _timeLimitUpgrade;
    [SerializeField] private UpgradeSelect _rewardUpgrade;
    [SerializeField] private MoneyStack _moneyStack;
    [SerializeField] private float _delayBeforeComplete;
    [Inject] private UpgradesController _upgradesController;
    [Inject] private Tutorial _tutorial;
    [Inject] private Timer _timer;
    
    [SerializeField, Dropdown(nameof(PlaceIds))]
    private string _requiredPlace;

    private string[] PlaceIds = PlaceId.All;
    private bool _requiredPlaceBought = false;

    public override float Reward => _upgradesController.GetValue(_rewardUpgrade);
    public float Progress { get; private set; }
    public UnityAction<float> ProgressChanged { get; set; }
    
    public float Duration => _upgradesController.GetValue(UpgradeIds.GuestPatience);

    public bool CanRequest()
    {
        if (_requiredPlaceBought)
            return true;
        
        return IsRequirePlaceBought();
    }

    protected override void OnUseItem()
    {
        DisableInteract();
        _timer.ExecuteWithDelay(Complete, _delayBeforeComplete);
    }
    
    private bool IsRequirePlaceBought()
    {
        _requiredPlaceBought = Place.GetSave(_requiredPlace).Bought;
        return _requiredPlaceBought;
    }
    
    protected override void OnRequest()
    {
        if (_tutorial.IsRunning == false)
        {
            SignalHub.Get<Signals.NewLimitedInTimeRequest>().Dispatch(this);
            StartCoroutine(RequestLimit());
        }
    }

    private IEnumerator RequestLimit()
    {
        float wastedTime = 0;
        while (wastedTime <= Duration)
        {
            if(_interactableCharacterZone.IsCharacterInside == false)
                wastedTime += Time.deltaTime;
            
            Progress = 1 - (wastedTime / Duration);
            ProgressChanged?.Invoke(Progress);
            
            yield return null;
        }

        Cancel();
    }

    protected override void OnDone()
    {
        if(_moneyStack == null)
            return;
        _moneyStack.Add(Reward);
    }

}
