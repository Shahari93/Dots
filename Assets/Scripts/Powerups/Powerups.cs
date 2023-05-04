using UnityEngine;
using Dots.Utils.Spawnable;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Powerups
{
    public abstract class Powerups : MonoBehaviour, ISpawnableObjects, IInteractableObjects
    {
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        float randX;
        float randY;
        float dotSpeed;
        Vector2 direction;

        void OnEnable()
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
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public abstract void BehaveWhenInteractWithPlayer();

        public void ShowDestroyParticles(bool? IsGoodDot)
        {
            GameObject particleGO = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleGO.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particleSystem.main;

            Color particlesColor = (bool)IsGoodDot ? Color.green : Color.red;
            main.startColor = particlesColor;

            particleSystem.Play();
            Destroy(particleSystem.gameObject, main.duration + 0.1f);
        }

        public float Speed { get => dotSpeed; set => dotSpeed = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
    }
}