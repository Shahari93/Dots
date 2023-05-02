using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public abstract class DotsBehaviour : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        float dotSpeed;
        Vector2 direction;

        public bool IsGoodDot { get; set; }

        void OnEnable()
        {
            dotSpeed = 80f;
            int randX = Random.Range(-180, 181);
            int randY = Random.Range(-180, 181);
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
                BehaveWhenIteractWithBounds();
            }
        }

        void SetSpeedAndDirection()
        {
            rb2D.velocity = dotSpeed * direction * Time.fixedDeltaTime;
        }

        // What happens if a dot hits the bounds collider
        void BehaveWhenIteractWithBounds()
        {
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public abstract void BehaveWhenIteractWithPlayer();

        public void ShowDestroyParticles(bool isGoodDot)
        {
            GameObject particleGO = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleGO.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule main = particleSystem.main;
            
            Color particlesColor = isGoodDot ? Color.green : Color.red;
            main.startColor = particlesColor;

            particleSystem.Play();
            Destroy(particleSystem.gameObject, main.duration + 0.1f);
        }
    }
}