using System;
using UnityEngine;
using Dots.Utils.Spawnable;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Powerups
{
    public abstract class Powerups : MonoBehaviour, ISpawnableObjects, IInteractableObjects
    {
        [SerializeField] protected float powerupDuration;
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        float randX;
        float randY;
        float powerupSpeed;
        Vector2 direction;

        public float Speed { get => powerupSpeed; set => powerupSpeed = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
        public Vector2 Direction { get => direction; set => direction = value; }

        public static Action OnCollectedPower;

        void OnEnable()
        {
            SetSpawnValues();
        }

        private void SetSpawnValues()
        {
            powerupSpeed = 80f;
            randX = UnityEngine.Random.Range(-180, 181);
            randY = UnityEngine.Random.Range(-180, 181);
            direction = new Vector2(randX, randY).normalized;
        }

        void FixedUpdate()
        {
            SetSpeedAndDirection();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenInteractWithBounds();
            }
        }

        public void SetSpeedAndDirection()
        {
            rb2D.velocity = powerupSpeed * direction * Time.fixedDeltaTime;
        }

        // What happens if a dot hits the bounds collider
        public void BehaveWhenInteractWithBounds()
        {
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public abstract void BehaveWhenInteractWithPlayer();

        public void ShowDestroyParticles(bool? isGoodDot)
        {
            GameObject particleGO = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleGO.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particleSystem.main;

            if (isGoodDot != null)
            {
                Color particlesColor = (bool)isGoodDot ? Color.green : Color.red;
                main.startColor = particlesColor;
                particleSystem.Play();
            }

            Destroy(particleSystem.gameObject, main.duration + 0.1f);
        }
    }
}