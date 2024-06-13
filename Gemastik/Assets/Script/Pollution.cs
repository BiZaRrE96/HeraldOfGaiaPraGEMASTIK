using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : MonoBehaviour
{
    private int pollutionAmount = 0;

    public void increase(int amount)
    {
        pollutionAmount += amount;
    }
    public void decrese(int amount)
    {
        pollutionAmount -= amount;
    }

}
