using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeTickSystem : MonoBehaviour
{
    public static Action TICK;
    public float TICK_TIME_MAX = .2f;

    private int tick;
    private float tickTimer;
    private void Awake()
    {
        tick = 0;
    }
    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= TICK_TIME_MAX)
        {
            tickTimer -= TICK_TIME_MAX;
            tick++;
            if (TICK != null)
            {
                TICK();
            }
        }
    }
}
