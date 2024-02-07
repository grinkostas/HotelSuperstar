using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : Dispenser
{
    protected override void OnBuy()
    {
    }

    protected override bool Takeable()
    {
        return true;
    }

    protected override Item Take()
    {
        var item = Instantiate(Item);
        item.transform.position = transform.position;
        return item;
    }
}
