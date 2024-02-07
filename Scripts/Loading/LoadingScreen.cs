using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] public Loading _loading;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _minDelay;
    [SerializeField, Range(0,1)] private float _minValueDelta;
    [SerializeField, Range(0,1)] private float _maxValueDelta;
    [SerializeField, Range(0,1)] private float _progressToChangeScreen;

    private float _currentProgress = 0.0f;
    private List<float> _progressQueue = new List<float>();

    private void OnEnable()
    {
        _loading.ProgressChanged += OnProgressChanged;
    }

    private void OnDisable()
    {
        _loading.ProgressChanged = OnProgressChanged;
    }

    private void Start()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        while (_currentProgress <= _progressToChangeScreen)
        {
            if (_progressQueue.Count > 0)
            {
                yield return LoadProgressStep(_progressQueue[0]);
                _progressQueue.Remove(_progressQueue[0]);
            }

            yield return null;
        }
        _loading.StartLoad();
    }

    private IEnumerator LoadProgressStep(float finalStep)
    {
        finalStep = Mathf.Clamp(finalStep, 0, _progressToChangeScreen);
        float startProgress = _currentProgress;
        float delta = finalStep - startProgress;
        if(delta < 0)
            yield break;
        if (delta < _minValueDelta)
        {
            _currentProgress = finalStep;
            _slider.value = _currentProgress;
            yield break;
        }

        float wastedTime = 0;
        float delay = _minDelay;
        if (delta > _maxValueDelta)
        {
            delay *= delta / _maxValueDelta;
        }
        while (wastedTime < delay)
        {
            wastedTime += Time.deltaTime;
            float progress = wastedTime / delay;
            _currentProgress = startProgress + progress * delta;
            _slider.value = _currentProgress;
            yield return null;
        }
        
        
    }
    
    private void OnProgressChanged(float progress)
    {
        _progressQueue.Add(progress);
    }
}
