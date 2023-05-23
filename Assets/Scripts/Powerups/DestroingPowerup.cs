using System;
using UnityEngine;
using Dots.Utils.Interaction;
using Dots.GamePlay.Powerups;
using Dots.Utils.Powerups.Objectpool;

namespace Dots.Utils.Destroy
{
    public class DestroingPowerup : MonoBehaviour, IDestroyableObject, IInteractableObjects
    {
        [SerializeField] protected ParticleSystem particles;
        [SerializeField] PowerupEffectSO powerupEffect;

        public static Action<float> OnCollectedPower;
        public static Action OnPowerupDisabled;


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenInteractWithBounds();
            }
        }

        // What happens if a dot hits the bounds collider
        public void BehaveWhenInteractWithBounds()
        {
            DisablePowerupVisuals();
            OnPowerupDisabled?.Invoke();
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public virtual void BehaveWhenInteractWithPlayer()
        {
            DisablePowerupVisuals();
            powerupEffect.Apply(this.gameObject);
        }

        public void DisablePowerupVisuals()
        {
            PowerupsSpawner.CanSpawn = true;
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }

        public void ShowDestroyParticles(bool? isGoodDot)
        {
            GameObject particleGO = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleGO.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particleSystem.main;
            if (isGoodDot == null)
            {
                Color particlesColor = Color.black;
                main.startColor = particlesColor;
            }

            particleSystem.Play();
            Destroy(particleSystem.gameObject, main.duration + 0.1f);
        }
    } 
}