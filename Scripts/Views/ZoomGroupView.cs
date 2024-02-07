using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAnalyticsSDK.Setup;

public class ZoomGroupView : View
{
    [SerializeField] private List<GameObject> _objectsToShow;
    [SerializeField] private Vector3 _startSize;
    [SerializeField] private float _duration = 0.5f;
    
    public override void Show()
    {
        Scale(Vector3.zero);
        ChangeActive(true);
        Animate(_startSize);
    }

    public override void Hide()
    {
        Animate(Vector3.zero);
    }
    

    private void Scale(Vector3 scale)
    {
        foreach (var gameObject in _objectsToShow)
        {
            gameObject.transform.localScale = Vector3.zero;
        }
    }

    private void ChangeActive(bool isActive)
    {
        foreach (var gameObject in _objectsToShow)
        {
            gameObject.SetActive(isActive);
        }
    }

    private void Animate(Vector3 scaleDestination)
    {
        foreach (var gameObject in _objectsToShow)
        {
            gameObject.transform.DOScale(scaleDestination, _duration);
        }
    }
}
