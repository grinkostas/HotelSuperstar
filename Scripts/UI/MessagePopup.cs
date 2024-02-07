using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Zenject;

public class MessagePopup : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private float _duration;
    [Inject] private Timer _timer;
    private bool _isHidden = true;
    
    public void Show(string message)
    {
        if(_isHidden == false)
            return;
        
        _isHidden = false;
        _messageText.text = message;
        _model.SetActive(true);
        _timer.ExecuteWithDelay(Hide, _duration);
    }

    private void Hide()
    {
        _isHidden = true;
        _model.SetActive(false);
    }
    
    
}
