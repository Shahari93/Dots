using UnityEngine;
using Dots.GamePlay.Dot.Good;
using System.Threading.Tasks;
using Dots.Utils.Powerups.Objectpool;

namespace Dots.GamePlay.Powerups.AllGreen
{
    public class AllGreensPowerup : Powerups
    {
        public override void BehaveWhenInteractWithPlayer()
        {
            base.BehaveWhenInteractWithPlayer();
            GoodDot.spawnChance = 1f;
        }

        public override void StartDurationTimerAndDisablePowerup()
        {
            powerupDuration -= Time.deltaTime;
            //while (powerupDuration >= 0) 
            //{
            //    await Task.Yield();
            //}
            if (powerupDuration <= 0f)
            {
                GoodDot.spawnChance = 0.15f;
            }

        }
    }
}