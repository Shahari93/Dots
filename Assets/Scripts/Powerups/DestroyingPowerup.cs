using System;
using UnityEngine;
using System.Collections;
using Dots.Audio.Manager;
using Dots.GamePlay.Powerups;
using CandyCoded.HapticFeedback;
using Dots.Utilities.Interface.Destroy;
using Dots.Utilities.Powerups.ObjectPool;
using Dots.Utilities.Interface.Interaction;

namespace Dots.Utilities.Destroy
{
    /// <summary>
    /// This class is for handling the logic of destroying the powerups
    /// It handles all the behaviors when colliding
    /// </summary>
    public class DestroyingPowerup : MonoBehaviour, IDestroyableObject, IInteractableObjects
    {
        [SerializeField] protected ParticleSystem particles;
        [SerializeField] PowerupEffectSO powerupEffect;
        [SerializeField] SpriteRenderer spriteRenderer;

        bool isFlicker = true;

        public static Action<float> OnCollectedPower;
        public static Action OnPowerupDisabled;

        void Start()
        {
            StartCoroutine(BlinkPowerupBeforeDisappear(5f));
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenInteractWithBounds();
            }
        }

        /// <summary> 
        /// What happens if a dot hits the bounds collider
        /// </summary>
        public void BehaveWhenInteractWithBounds()
        {
            DisableVisuals();
            OnPowerupDisabled?.Invoke();
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public virtual void BehaveWhenInteractWithPlayer()
        {
            if (SettingMenuPresenter.IsHapticOn)
            {
                HapticFeedback.MediumFeedback();
            }

            AudioManager.Instance.PlaySFX("CollectedPowerup");
            DisableVisuals();
            powerupEffect.Apply(this.gameObject);
        }
        /// <summary>
        /// Disabling the powerup visuals
        /// </summary>
        public void DisableVisuals()
        {
            PowerupsSpawner.CanSpawn = true;
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }

        public IEnumerator BlinkPowerupBeforeDisappear(float duration)
        {
            if (duration <= 0)
            {
                yield break;
            }

            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
                if (duration <= duration / 2 && isFlicker)
                {
                    spriteRenderer.color = new Color(255,255,255, 255 / 2);
                    yield return new WaitForSecondsRealtime(0.5f);
                    spriteRenderer.color = Color.white;
                    yield return new WaitForSecondsRealtime(0.5f);
                }
                if (duration <= 0)
                {
                    PowerupsSpawner.CanSpawn = true;
                    gameObject.SetActive(false);
                    isFlicker = false;
                    break;
                }
            }
        }

        /// <summary>
        /// Showing particles when the dot is being collided with
        /// </summary>
        /// <param name="isGoodDot">If this bool is true we show the green dot particles, if not the red dot particles.
        /// If null we show the powerup particles
        /// </param>
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