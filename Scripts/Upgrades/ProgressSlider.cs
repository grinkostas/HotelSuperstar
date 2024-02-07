using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ProgressSlider : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _fillImage;
    [SerializeField] private float _spacing;
    [SerializeField] private float _minValue = 0.1f;
    [SerializeField] private Gradient _gradient;
    [SerializeField, Range(0.001f, 1f)] private float _value;

    private float MaxWidth => _background.rectTransform.rect.width;
    
    public float Value
    {
        get => _value;
        set
        {
            _value = Mathf.Clamp(value, _minValue, 1);
            float width = (MaxWidth-_spacing) * _value;
            _fillImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width - _spacing);
            _fillImage.rectTransform.anchoredPosition = new Vector2((width/2) + _spacing/2, 0);
            _fillImage.color = _gradient.Evaluate(_value);
        }
    }

    private void OnValidate()
    {
        Value = _value;
    }

}
