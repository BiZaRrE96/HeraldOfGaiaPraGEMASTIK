using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Belt : Buildcost
{
    public string reservecon;
    public int item = 0;
    public GameObject item_object;
    public abstract void Tick();
}
