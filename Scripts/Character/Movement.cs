using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using Zenject;

public abstract class Movement : MonoBehaviour
{
    
    [SerializeField] private bool _speedFromUpgrade = true;
    [SerializeField, ShowIf(nameof(_speedFromUpgrade))] private UpgradeSelect _speedSelect;
    [SerializeField, HideIf(nameof(_speedFromUpgrade))] private float _speed;
    
    [Inject] private UpgradesController _upgradesController;
    private List<SpeedMultiplier> _speedMultipliers = new List<SpeedMultiplier>();
    
    private const float Tolerance = 0.1f;
    
    protected UpgradeModel SpeedModel => _speedFromUpgrade ? _upgradesController.GetModel(_speedSelect) : null;
    
    protected float Speed => _speedFromUpgrade ? SpeedModel.CurrentValue : _speed;
    
    public float SpeedMultipliersAggregated
    {
        get
        {
            if (_speedMultipliers.Count == 0) return 1.0f;
            return _speedMultipliers.Select(x => x.Value).Aggregate((sum, current) => sum * current);
        }
    }


    public void ApplyMultiplayer(object sender, float multiplier)
    {
        var speedMultiplier = _speedMultipliers.Any(x => x.Sender == sender && Mathf.Abs(x.Value - multiplier) < Tolerance);
        if (speedMultiplier)
            return;
        _speedMultipliers.Add(new SpeedMultiplier(sender, multiplier));
        OnAppliedMultiplayer();
    }

    public void RemoveMultiplayer(object sender, float multiplierValue)
    {
        var multiplier = _speedMultipliers.Find(x => x.Sender == sender && Mathf.Abs(x.Value - multiplierValue) < Tolerance);

        if (multiplier != null)
        {
            _speedMultipliers.Remove(multiplier);
            OnRemovedMultiplayer();
        }
    }
    
    protected virtual void OnAppliedMultiplayer(){}
    protected virtual void OnRemovedMultiplayer(){}
    
    
    public class SpeedMultiplier
    {
        public object Sender;
        public float Value;

        public SpeedMultiplier(object sender, float value)
        {
            Sender = sender;
            Value = value;
        }
    }
}
