using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [SerializeField]
    private int energyAmount = 0;

    public void increase(int amount)
    {
        energyAmount += amount;
    }
    public void decrese(int amount)
    {
        energyAmount -= amount;
    }
    public bool checkenergy(int amount)
    {
        if (amount <= energyAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
