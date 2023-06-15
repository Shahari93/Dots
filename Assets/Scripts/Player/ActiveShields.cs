using UnityEngine;
using Dots.Ads.Init;
using Dots.Utils.Destroy;
using Dots.GamePlay.Powerups.Shield;
using Dots.Utils.Interface.Interaction;

namespace Dots.GamePlay.Player.Interaction.Shields
{
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
            }
        }

        void EnableShieldsVisual(bool isShieldOn)
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

        bool IsShieldFromRV()
        {
            if (IronSourceInit.IsShieldFromRV)
            {
                DestroingPowerup.OnCollectedPower?.Invoke(0);
                return true;
            }
            else
            {
                return false;
            }
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