using UnityEngine;
using UnityEngine.Events;

public class UpgradeModel
{
    private int _currentLevel;
    private Upgrade _upgrade;
    
    public float CurrentValue => _upgrade.GetValue(_currentLevel);
    public int CurrentLevel => _currentLevel;
    public int MaxLevel => _upgrade.MaxLevel;
    public Upgrade Upgrade => _upgrade;

    public UnityAction Upgraded;

    public UpgradeModel(Upgrade upgrade)
    {
        _upgrade = upgrade;
        _currentLevel = ES3.Load<int>(_upgrade.Id, 0);
    }

    public bool CanLevelUp()
    {
        return _currentLevel + 1 <= _upgrade.MaxLevel;
    }
    
    public void LevelUp()
    {
        if(CanLevelUp() == false)
            return;
        
        _currentLevel++;
        ES3.Save(_upgrade.Id, _currentLevel);
        Upgraded?.Invoke();
    }
}
