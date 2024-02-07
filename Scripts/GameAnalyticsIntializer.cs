using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;

public class GameAnalyticsIntializer : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
    }
}
