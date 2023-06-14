using System.Collections;
using UnityEngine;

namespace Dots.Utils.GeneralTimer
{
    public class Timer
    {
        public float time;
        public Timer(float time)
        {
            this.time = time;
        }

        public IEnumerator CountDown()
        {
            while (time > 0)
            {
                time -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            if (time < 0)
            {
                time = 0;
                yield return null;
            }   
        }
    }
}