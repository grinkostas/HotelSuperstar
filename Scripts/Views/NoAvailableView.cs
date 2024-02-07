using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoAvailableView : View
{
    [SerializeField] private float _duration;

    public override void Show()
    {
        base.Show();
        StartCoroutine(Timer(_duration));
    }

    private IEnumerator Timer(float duration)
    {
        yield return new WaitForSeconds(_duration);
        Hide();
    }
}
