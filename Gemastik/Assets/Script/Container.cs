using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public int storage = 0;
    public int item = 0;

    public void input(int item)
    {
        if (this.item == 0)
        {
            this.item = item;
            storage++;
        }
        else
        {
            storage++;
        }
    }
}
