using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class View : MonoBehaviour
{
    [SerializeField] protected GameObject Model;
    protected bool IsHidden => Model.activeSelf;
    
    public virtual void Show()
    {
        if(!Model.activeSelf) Model.SetActive(true);
    }

    public virtual void Hide()
    {
        Model.SetActive(false);
    }

}
