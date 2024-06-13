using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour,Building
{
    public static List<int> storage = new List<int>(new int[10]);
    public bool checker(GameObject gameObject,int produce)
    {
        return true;
    }
    public void inputer(int Produce)
    {
        storage[Produce]++;
        Debug.Log(storage[Produce]);
    }
    private void Update()
    {
        int i=1;
        foreach(int nt in storage)
        {
            Debug.Log("storage "+i+" "+nt);
            i++;
        }
    }
    public void disableScript()
    {
    }

    public void enableScript()
    {
    }

}
