using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class RequestHint : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _countText;
    [SerializeField] private CircleLoader _loader;
    
    private List<LimitedInTimeRequest> _requests = new List<LimitedInTimeRequest>();
    public Item Item { get; private set; }

    public void AddRequest(LimitedInTimeRequest request)
    {
        if (_requests.Count == 0)
        {
            gameObject.SetActive(true);
            Item = request.RequiredItem;
            _icon.sprite = Item.Icon;
        }

        _requests.Add(request);
        request.End += OnRequestEnd;
        Actualize();
    }

    private void OnRequestEnd(Request request)
    {
        var limitedInTimeRequest = (LimitedInTimeRequest)request;
        limitedInTimeRequest.ProgressChanged -= OnRequestProgressChanged;
        limitedInTimeRequest.End -= OnRequestEnd;
        _requests.Remove(limitedInTimeRequest);
        Actualize();
    }

    private void Actualize()
    {
        if (_requests.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
        _countText.text = _requests.Count.ToString();
        float minProgress = _requests.Max(x => x.Progress);
        var shortestRequest = _requests.Find(x => Math.Abs(x.Progress - minProgress) < 0.1f);
        OnRequestProgressChanged(minProgress);
        shortestRequest.ProgressChanged += OnRequestProgressChanged;
    }

    private void OnRequestProgressChanged(float progress)
    {
        _loader.SetProgress(progress);
    }
    
}
