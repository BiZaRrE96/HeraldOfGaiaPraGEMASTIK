using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int coinAmount = 0;

    public void increase(int amount)
    {
        coinAmount += amount;
    }
    public void decrese(int amount)
    {
        coinAmount -= amount;
    }
    public bool checkenergy(int amount)
    {
        if (amount >= coinAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
