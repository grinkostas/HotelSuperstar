using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class GeneratorView : MonoBehaviour
{
    [SerializeField] private Generator _generator;
    [SerializeField] private TMP_Text _generatedText;
    [SerializeField] private TMP_Text _capacityText;

    private void OnEnable()
    {
        _generator.ItemsCountChanged += Actualize;
        _generator.CapacityModel.Upgraded += Actualize;
        Actualize();
    }

    private void OnDisable()
    {
        _generator.ItemsCountChanged -= Actualize;
        _generator.CapacityModel.Upgraded -= Actualize;
    }


    private void Actualize()
    {
        _generatedText.text = _generator.GeneratedCount.ToString();
        _capacityText.text = ((int)_generator.CapacityModel.CurrentValue).ToString();
    }
}
