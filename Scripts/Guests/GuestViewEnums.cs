using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class GuestView
{
    [System.Serializable]
    public class GuestViewTransition
    {
        public GuestViewCondition Condition;
        public GuestViewAction Action;
    }
    public enum GuestViewCondition
    {
        StartCheckIn,
        CheckedIn,
        End
    }

    public enum GuestViewAction
    {
        Hide,
        Show
    }
}
