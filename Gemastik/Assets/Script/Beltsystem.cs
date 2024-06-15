using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beltsystem : MonoBehaviour
{
    [SerializeField]
    List<Belt> belts = new List<Belt>();

    // Start is called before the first frame update
    void Start()
    {
        TimeTickSystem.TICK += Tick;
    }

    // Called on each tick to process items
    private void Tick()
    {
        int j=0;
        for (int i = belts.Count - 1; i >= 0; i--)
        {
            Belt belt = belts[i];
            if (belt)
            {
                belt.Tick();
                Debug.Log(belt.gameObject.name + " " + j);
                j++;
            }
        }
    }
    public void addConveyer(Belt conveyer)
    {
        belts.Add(conveyer);
    }
    public void removeConveyer(Belt conveyer)
    {
        belts.Remove(conveyer);
    }
}
