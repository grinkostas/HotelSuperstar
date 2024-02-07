using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Zenject;

public class UpgradesController : MonoBehaviour
{
    [SerializeField] private List<Upgrade> _upgrades;
    [Inject] private SignalHub _signalHub;
    private Dictionary<string, UpgradeModel> _models = new Dictionary<string, UpgradeModel>();
    
    private void Start()
    {
        _signalHub.Get<Signals.UpgradeLevelUp>().On(LevelUp);
    }

    private void LevelUp(Upgrade upgrade)
    {
        GetModel(upgrade).LevelUp();
    }

    private Upgrade GetConfig(string id)
    {
        return _upgrades.Find(x => x.Id == id);
    }

    
    public UpgradeModel GetModel(string id)
    {
        if (_models.ContainsKey(id))
            return _models[id];
        
        var model = new UpgradeModel(GetConfig(id));
        _models.Add(id, model);

        return model;
    }

    public UpgradeModel GetModel(UpgradeSelect select) => GetModel(select.Text);
    public UpgradeModel GetModel(Upgrade upgrade) => GetModel(upgrade.Id);

    public float GetValue(UpgradeSelect select) => GetValue(select.Text);

    public float GetValue(string id)
    {
        return GetModel(id).CurrentValue;
    }
    
}
