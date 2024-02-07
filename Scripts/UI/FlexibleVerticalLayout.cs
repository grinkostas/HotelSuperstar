using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FlexibleVerticalLayout : VerticalLayoutGroup
{
    protected override void OnTransformChildrenChanged()
    {
        base.OnTransformChildrenChanged();
        float height = 0;
        foreach (Transform children in transform)
        {
            height += children.GetComponent<RectTransform>().sizeDelta.y + spacing;
        }

        rectTransform.sizeDelta = new Vector3(rectTransform.sizeDelta.x, height);
    }
}
