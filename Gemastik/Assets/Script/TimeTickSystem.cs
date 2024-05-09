using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeTickSystem : MonoBehaviour
{
    public class onTickEvent : EventArgs
    {
        public int tick;
    }
    public static EventHandler<onTickEvent> onTick;  
    private const float TICK_TIME_MAX = .2f;

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
            if (onTick != null)
            {
                onTick(this, new onTickEvent
                {
                    tick = tick
                });
            }
        }
    }
}
