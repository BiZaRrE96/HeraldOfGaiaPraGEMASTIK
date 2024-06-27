using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pollution : MonoBehaviour
{
    private static int pollutionAmount = 0;
    [SerializeField]
    private Slider slider;
    public static void increase(int amount)
    {
        pollutionAmount += amount;
    }
    public static void decrese(int amount)
    {
        pollutionAmount -= amount;
    }
    private void Update()
    {
        slider.value = pollutionAmount;
    }
}
