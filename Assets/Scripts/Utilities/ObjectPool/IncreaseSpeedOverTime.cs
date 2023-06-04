using System;
using UnityEngine;

namespace Dots.GamePlay.Dot.Timer
{
    public class IncreaseSpeedOverTime : MonoBehaviour
    {
        int tick;
        float tickTimer;
        const float TICK_TIMER_MAX = 15f;

        public static event Action<int> OnTickIncreased;

        void Awake()
        {
            tick = 0;
        }

        void Update()
        {
            IncreaseTicks();
        }

        void IncreaseTicks()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= TICK_TIMER_MAX)
            {
                tickTimer -= TICK_TIMER_MAX;
                tick++;
                OnTickIncreased?.Invoke(tick);
            }
        }
    }
}