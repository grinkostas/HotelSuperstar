using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class CircleLoader : MonoBehaviour
{
    [SerializeField] private bool _valueOutside;
    [SerializeField, RequireInterface(typeof(IProgressible)), HideIf(nameof(_valueOutside))]
    private GameObject _progressibleObject;
    
    [SerializeField] private Image _image;
    [SerializeField] private bool _hideOnInactive;
    [ShowIf(nameof(_hideOnInactive)), SerializeField] private GameObject _model;

    [SerializeField] private bool _multipleColor;

    [ShowIf(nameof(_multipleColor))] [SerializeField] private Gradient _gradient;

    private IProgressible _progressible;
   
    private void OnEnable()
    {
        if(_progressibleObject == null)
            return;
        _progressible = _progressibleObject.GetComponent<IProgressible>();
        _progressible.ProgressChanged += ShowProgress;
    }

    private void Start()
    {
        if(_progressibleObject == null)
            return;
        ResetProgress();
    }

    private void OnDisable()
    {
        if(_progressibleObject == null)
            return;
        _progressible.ProgressChanged -= ShowProgress;
    }
    private void ShowProgress(float progress)
    {
        if(_progressibleObject == null)
            return;
        if(progress > 0)
            _model.SetActive(true);
        
        if(progress == 0 && _hideOnInactive && _model != null)
            _model.SetActive(false);
        
        _image.fillAmount = progress;

        if (_multipleColor)
            _image.color = _gradient.Evaluate(progress);
    }

    public void SetProgress(float progress)
    {
        _image.fillAmount = progress;

        if (_multipleColor)
            _image.color = _gradient.Evaluate(progress);
    }
    

    private void ResetProgress()
    {
        if(_progressibleObject == null)
            return;
        _image.fillAmount = 0;
        if(_hideOnInactive && _model != null)
            _model.SetActive(false);
    }
}
