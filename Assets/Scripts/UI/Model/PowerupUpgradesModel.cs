using UnityEngine;

namespace Dots.Powerup.Model
{
	public class PowerupUpgradesModel : MonoBehaviour
	{
		public static PowerupUpgradesModel Instance;

        private void Awake()
        {
            if(Instance != null) 
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        static float powerupDurationValue;
        public static float PowerupDurationValue
        {
            get
            {
                return powerupDurationValue;
            }
            set
            {
                powerupDurationValue = value;
            }
        }

        static int coinsCost;
        public static int CoinsCost
        {
            get
            {
                return coinsCost;
            }
            set
            {
                coinsCost = value;
            }
        }
    } 
}