using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ElevatorLiftPopup : Popup
{
    [SerializeField] private List<ElevatorLiftButton> _elevatorLiftButtons;
    [SerializeField] private Elevator _elevator;
    [SerializeField] private GameObject _objectToShow;
    
    protected override GameObject ObjectToShow => _objectToShow;
    public override void Show()
    {
        base.Show();
        foreach (var elevatorLiftButton in _elevatorLiftButtons)
        {
            elevatorLiftButton.OnClick += OnLiftButtonClick;
        }
    }

    private void OnLiftButtonClick(Floor floor)
    {
        _elevator.Elevate(floor);   
        Hide();
    }
}
