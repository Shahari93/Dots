using UnityEngine;

namespace Dots.Utils.FTUE
{
    public class CheckForFTUE : MonoBehaviour
    {
        static int launchCount;
        public static int LaunchCount
        {
            get
            {
                return launchCount;
            }
            set
            {
                launchCount = value;
            }
        }
        void Awake()
        {
            IncrementLaunchCount();
        }

        void IncrementLaunchCount()
        {
            // check if the key exists.
            // if so, add to count
            if (PlayerPrefs.HasKey("LaunchCount"))
            {
                // get the current count
                launchCount = PlayerPrefs.GetInt("LaunchCount");
                // increment the count
                launchCount += 1;
                // set to PlayerPrefs
                PlayerPrefs.SetInt("LaunchCount", launchCount);
            }
            // if not, first time launched, add key
            else
            {
                PlayerPrefs.SetInt("LaunchCount", 1);
            }
        }
    } 
}