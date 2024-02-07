using System;
using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Player : InteractableCharacter
{
    [SerializeField] private Balance _balance;
    [SerializeField] private Transform _moneyPoint;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerAnimator _playerAnimator;
    
    public PlayerAnimator Animator => _playerAnimator;
    public PlayerMovement Movement => _movement;
    public Transform MoneyPoint => _moneyPoint;
    public Balance Balance => _balance;

}
