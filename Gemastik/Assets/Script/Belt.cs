using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Belt : MonoBehaviour
{
    public string reservecon;
    public int item = 0;
    public int spareitem = 0;
    public abstract void Tick();
}
