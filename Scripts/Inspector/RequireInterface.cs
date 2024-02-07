using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RequireInterface : PropertyAttribute
{
    public readonly System.Type RequireType;

    public RequireInterface (System.Type requireType)
    {
        RequireType = requireType;
    }
}


