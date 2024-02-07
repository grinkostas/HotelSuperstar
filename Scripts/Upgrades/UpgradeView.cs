using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _currentValue;
    [SerializeField] private TMP_Text _deltaText;

    [SerializeField] private GameObject _hideIfMax;
    [SerializeField] private GameObject _showIfMax;
    
    [SerializeField] private UpgradeProgressBarView _progressBarView;
    
    private UpgradeModel _upgradeModel;
    public bool IsMax { get; private set; }
    public void Initialize(UpgradeModel upgrade)
    {
        if (_upgradeModel != null)
        {
            Actualize();
            return;
        }
        _upgradeModel = upgrade;
        _deltaText.text = _upgradeModel.Upgrade.Property.Step.ToString();
        _nameText.text = _upgradeModel.Upgrade.Name;
        _upgradeModel.Upgraded += Actualize;
        Actualize();
    }

    private void OnEnable()
    {
        if (_upgradeModel != null)
            _upgradeModel.Upgraded += Actualize;
        Actualize();
    }

    private void Start()
    {
        Actualize();
    }

    private void OnDisable()
    {
        if (_upgradeModel != null)
            _upgradeModel.Upgraded -= Actualize;
    }

    public void Actualize()
    {
        if(_upgradeModel == null)
            return;
        
        if (_upgradeModel.CanLevelUp() == false)
        {
            IsMax = true;
            _hideIfMax.SetActive(false);
            _showIfMax.SetActive(true);
        }
        else
        {
            _hideIfMax.SetActive(true);
            _showIfMax.SetActive(false);
            _currentValue.text = _upgradeModel.CurrentValue.ToString();
        }
        _progressBarView.Initialize(_upgradeModel);
    }
}
