using System;
using UnityEngine;

namespace Dots.GamePlay.Dot.Timer
{
    public class IncreaseSpeedOverTime : MonoBehaviour
    {
        const float TICK_TIMER_MAX = 15f;
        int tick;
        float tickTimer;

        public static event Action<int> OnTenSecondsPassed;

        private void Awake()
        {
            tick = 0;
        }

        void Update()
        {
            IncreaseTicks();
        }

        private void IncreaseTicks()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= TICK_TIMER_MAX)
            {
                tickTimer -= TICK_TIMER_MAX;
                tick++;
                OnTenSecondsPassed?.Invoke(tick);
            }
        }
    }
}