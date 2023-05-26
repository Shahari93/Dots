using UnityEngine;
using Dots.Ads.Init;
using Dots.Utils.Interaction;
using Dots.Utils.Destroy;
using Dots.GamePlay.Powerups.Shield;

namespace Dots.GamePlay.Player.Interaction.Shields
{
    public class ActiveShields : MonoBehaviour
    {
        [SerializeField] GameObject[] shields;

        private static bool areShieldsActive;
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

        private void OnEnable()
        {
            ShieldPowerup.OnCollectedShieldPowerup += EnableShieldsVisual;
            IronSourceInit.OnShieldRvWatched += IsShieldFromRV;
        }

        private void Awake()
        {
            if (!IsShieldFromRV())
            {
                areShieldsActive = false;
                foreach (GameObject shield in shields)
                {
                    shield.SetActive(false);
                }
            }
            else
            {
                areShieldsActive = true;
                foreach (GameObject shield in shields)
                {
                    shield.SetActive(true);
                }
            }
        }

        private void EnableShieldsVisual(bool isShieldOn)
        {
            areShieldsActive = isShieldOn;
            foreach (GameObject shield in shields)
            {
                shield.SetActive(isShieldOn);
            }
            if (!areShieldsActive)
            {
                DestroingPowerup.OnPowerupDisabled?.Invoke();
            }
        }

        private bool IsShieldFromRV()
        {
            DestroingPowerup.OnCollectedPower?.Invoke(0);
            return IronSourceInit.IsShieldFromRV = true;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IInteractableObjects interactable))
            {
                interactable.BehaveWhenInteractWithPlayer();
            }
        }

        private void OnDisable()
        {
            ShieldPowerup.OnCollectedShieldPowerup -= EnableShieldsVisual;
            IronSourceInit.OnShieldRvWatched -= IsShieldFromRV;
        }
    }
}