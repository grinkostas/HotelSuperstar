using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class UpgradeProgressBarView : MonoBehaviour
{
    [SerializeField] private ProgressSlider _progressSlider;

    private UpgradeModel _upgradeModel;
    
    public void Initialize(UpgradeModel model)
    {
        _upgradeModel = model;

        Actualize();

        model.Upgraded += OnModelUpgraded;
    }

    private void Actualize()
    {
        float value = (float)_upgradeModel.CurrentLevel / (float)_upgradeModel.MaxLevel;
        _progressSlider.Value = value;
    }

    private void OnEnable()
    {
        if (_upgradeModel != null && _upgradeModel.CanLevelUp())
            _upgradeModel.Upgraded += OnModelUpgraded;
    }

    private void OnDisable()
    {
        if (_upgradeModel != null)
            _upgradeModel.Upgraded -= OnModelUpgraded;
    }

    private void OnModelUpgraded()
    {
        Actualize();
        if (_upgradeModel.CanLevelUp() == false)
        {
            _progressSlider.Value = 1;
            _upgradeModel.Upgraded -= OnModelUpgraded;
        }
    }

}
