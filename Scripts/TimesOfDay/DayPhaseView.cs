using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayPhaseView : TimeOfDayBase
{
    [SerializeField] private View _view;
    
    protected override void OnSuitablePhase()
    {
        _view.Show();
    }

    protected override void OnNotSuitablePhase()
    {
        _view.Hide();
    }
}
