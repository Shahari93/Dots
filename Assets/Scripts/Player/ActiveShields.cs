using UnityEngine;
using Dots.Ads.Init;
using Dots.Utilities.Destroy;
using Dots.GamePlay.Powerups.Shield;
using Dots.Utilities.Interface.Interaction;
using Dots.Utilities.Powerups.ObjectPool;

namespace Dots.GamePlay.Player.Interaction.Shields
{
    /// <summary>
    /// This class is responsible for the player shield visuals.
    /// Checking if the player watch shield RV before the start of the game
    /// And if the player collected a shield powerup
    /// If yes, we enable the shields and the player gets 1 "extra" life
    /// </summary>
    public class ActiveShields : MonoBehaviour
    {
        [SerializeField] GameObject[] shields;

        static bool areShieldsActive;
        public static bool AreShieldsActive
        {
            get
            {
                return areShieldsActive;
            }
            set
            {
                areShieldsActive = value;
            }
        }

        void OnEnable()
        {
            ShieldPowerup.OnCollectedShieldPowerup += EnableShieldsVisual;
            IronSourceInit.OnShieldRvWatched += IsShieldFromRV;
        }

        void Awake()
        {
            if (!IsShieldFromRV())
            {
                areShieldsActive = false;
                foreach (GameObject shield in shields)
                {
                    shield.SetActive(areShieldsActive);
                }
            }
            else
            {
                areShieldsActive = true;
                foreach (GameObject shield in shields)
                {
                    shield.SetActive(areShieldsActive);
                    EnableShieldsVisual(areShieldsActive);
                }
                IronSourceInit.IsShieldFromRV = !IsShieldFromRV();
                DestroyingPowerup.OnCollectedPower?.Invoke(0);
            }
        }

        /// <summary>
        /// Settings the shield visuals based on the boolean parameter we get
        /// </summary>
        /// <param name="isShieldOn">if true, the shields visuals are on. If false, the visuals are off</param>
        void EnableShieldsVisual(bool isShieldOn)
        {
            areShieldsActive = isShieldOn;
            foreach (GameObject shield in shields)
            {
                shield.SetActive(isShieldOn);
            }
            if (!areShieldsActive)
            {
                DestroyingPowerup.OnPowerupDisabled?.Invoke();
                PowerupsSpawner.CanSpawn = true;
            }
        }

        /// <summary>
        /// Checking if the player watched the shield RV before he started the game
        /// </summary>
        /// <returns> return true or false based on if boolean from IronSourceInit class</returns>
        bool IsShieldFromRV()
        {
            DestroyingPowerup.OnCollectedPower?.Invoke(0);
            return IronSourceInit.IsShieldFromRV;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IInteractableObjects interactable))
            {
                interactable.BehaveWhenInteractWithPlayer();
            }
        }

        void OnDisable()
        {
            ShieldPowerup.OnCollectedShieldPowerup -= EnableShieldsVisual;
            IronSourceInit.OnShieldRvWatched -= IsShieldFromRV;
        }
    }
}