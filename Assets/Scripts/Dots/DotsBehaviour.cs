using UnityEngine;
using Dots.Utils.Spawnable;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public abstract class DotsBehaviour : MonoBehaviour, IInteractableObjects, ISpawnableObjects
    {
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        float randX;
        float randY;
        float dotSpeed;
        Vector2 direction;

        public float Speed { get => dotSpeed; set => dotSpeed = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        protected bool IsGoodDot { get; set; }

        void OnEnable()
        {
            SetSpawnValues();
        }

        private void SetSpawnValues()
        {
            dotSpeed = 80f;
            randX = Random.Range(-180, 181);
            randY = Random.Range(-180, 181);
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
            rb2D.velocity = dotSpeed * direction * Time.fixedDeltaTime;
        }

        // What happens if a dot hits the bounds collider
        public void BehaveWhenInteractWithBounds()
        {
            ShowDestroyParticles(IsGoodDot);
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

            Color particlesColor = (bool)isGoodDot ? Color.green : Color.red;
            main.startColor = particlesColor;

            particleSystem.Play();
            Destroy(particleSystem.gameObject, main.duration + 0.1f);
        }
    }
}