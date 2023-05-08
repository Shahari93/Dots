using System;
using UnityEngine;
using Dots.Utils.Spawnable;
using Dots.Utils.Interaction;
using Dots.Utils.Powerups.Objectpool;

namespace Dots.GamePlay.PowerupsPerent
{
    public abstract class Powerups : MonoBehaviour, IInteractableObjects, ISpawnableObjects
    {
        [SerializeField] protected float powerupDuration;
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        public static event Action<float> OnCollectedPower;

        float randX;
        float randY;
        float spawnSpeed;
        Vector2 direction;

        public float Speed { get => spawnSpeed; set => spawnSpeed = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
        public Vector2 Direction { get => direction; set => direction = value; }

        void Awake()
        {
            SetSpawnValues();
        }

        void SetSpawnValues()
        {
            spawnSpeed = 80f;
            randX = UnityEngine.Random.Range(-180, 181);
            randY = UnityEngine.Random.Range(-180, 181);
            direction = new Vector2(randX, randY).normalized;
        }

        public void SetSpeedAndDirection()
        {
            rb2D.velocity = spawnSpeed * direction * Time.fixedDeltaTime;
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

        // What happens if a dot hits the bounds collider
        public void BehaveWhenInteractWithBounds()
        {
            DisablePowerupVisuals();
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public virtual void BehaveWhenInteractWithPlayer()
        {
            DisablePowerupVisuals();
            OnCollectedPower?.Invoke(powerupDuration);
        }

        private void DisablePowerupVisuals()
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