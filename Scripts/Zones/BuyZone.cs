using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
using Zenject;

public class BuyZone : Zone<Player>
{
    [SerializeField] private float _timeForBuy = 1.5f;
    [SerializeField] private Place _place;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private List<BuyZone> _linkedZones;
    [SerializeField] private float _interactTime = 1.0f;
    [SerializeField] private float _buyingStartDelay;
    [Header("Animation")] 
    [SerializeField] private Vector3 _scaleMultiplayer;
    [SerializeField] private float _animationDuration;
    [SerializeField] private Image _zoneBorderImage;
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Transform _moneyPayPoint;

    [Inject] private Shop _shop;

    private float CoinsPerSecond => _place.Price / _timeForBuy;
    
    private Vector3 _startScale;

    private float _currentProgress = 0;

    public override float InteractTime => _interactTime;
    public Place Place => _place;
    private float CurrentProgress
    {
        get => _currentProgress;
        set
        {
            _currentProgress = value;
            _priceText.text = ((int)(_place.Price - _currentProgress)).ToString();
        }
    }
    private void Start()
    {
        _startScale = _zoneBorderImage.rectTransform.localScale;
        UpdateProgress();
    }

    public void UpdateProgress()
    {
        CurrentProgress = _place.GetSave().BuyProgress;
    }

    private void OnDisable()
    {
        _place.Bought -= OnPlaceBought;
    }

    protected override bool CanInteract(Player player)
    {
        return player.Balance.Amount > 0;
    }

    protected override void OnInteract(Player player)
    {
        StartCoroutine(Buying(player));
    }

    private IEnumerator Buying(Player player)
    {
        yield return new WaitForSeconds(_buyingStartDelay);
        while (IsCharacterInside)
        {
            if (InteractableCharacter.Balance.Amount < 1.0f)
            {
                player.Animator.StopPayMoney();
                yield break;
            }

            yield return null;
            if(player.Movement.IsMoving)
                continue;
            yield return Pay(player);
            
            
            if (CurrentProgress >= _place.Price)
            {
                _place.Buy();
                player.Animator.StopPayMoney();
                OnPlaceBought();
            }
        }
    }

    private float _currentWastedMoney = 0.0f;

    private IEnumerator Pay(Player player)
    {
        player.Animator.PayMoney(_moneyPayPoint);
        float coins = CoinsPerSecond * Time.deltaTime;
        _currentWastedMoney += coins;
        _shaker.Shake();
        if(player == null)
            yield break;
        if (_currentWastedMoney >= 1.0f)
        {
            float spent = (int)_currentWastedMoney;
            player.Balance.Spend(spent);
            _currentWastedMoney -= spent;
            CurrentProgress = Mathf.Clamp(CurrentProgress + spent, 0, _place.Price);
        }
    }
    
    private void OnPlaceBought()
    {
        gameObject.SetActive(false);
    }

    protected override void OnPlayerEnter(Player player)
    {
        Animation(Vector3.Scale(_startScale, _scaleMultiplayer));
    }

    protected override void OnPlayerExit(Player player)
    {
        Animation(_startScale);
        player.Animator.StopPayMoney();
        _place.Save((int)CurrentProgress);
        UpdateProgress();
        _linkedZones.ForEach(x=> x.UpdateProgress());
    }

    private void Animation(Vector3 destination)
    {
        _zoneBorderImage.rectTransform.DOScale(destination, _animationDuration);
    }
}
