using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "ScriptableObjects/Plants", order = 1)]
public class Plant : Item
{
    [SerializeField] private int growTime;

    public int GrowTime
    {
        get { return growTime; }
        set { growTime = value; }
    }
}