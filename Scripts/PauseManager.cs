using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseManager : MonoBehaviour
{
    private List<object> _blockers = new List<object>();

    public void Pause(object objectId)
    {
        _blockers.Add(objectId);
        Time.timeScale = 0;
    }

    public void Resume(object objectId)
    {
        if (_blockers.Contains(objectId))
            _blockers.Remove(objectId);
        
        if(_blockers.Count > 0) 
            return;

        Time.timeScale = 1;
    }
}
