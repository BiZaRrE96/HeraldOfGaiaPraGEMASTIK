using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : Buildcost, Building
{
    public bool checker(GameObject gameObject, int produce)
    {
        return false;
    }

    public void disableScript()
    {
        this.GetComponentInChildren<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    public void enableScript()
    {
        this.GetComponentInChildren<BoxCollider2D>().enabled=true;
    }

    public void inputer(int Produce, GameObject resource)
    {

    }

}
