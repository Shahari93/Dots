using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public class DotsBehaviour : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] ParticleSystem particles;
        [SerializeField] Rigidbody2D rb2D;

        float speed;
        Vector2 direction;

        public bool IsGoodDot { get; set; }

        void OnEnable()
        {
            speed = 80f;
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
            rb2D.velocity = speed * direction * Time.fixedDeltaTime;
        }
         
        // TODO: Find a way to refactor those 2 methods because they do the same thing 
        // What happens if a dot hits the bounds collider
        void BehaveWhenIteractWithBounds()
        {
            DisableComponnetsWhenDestroied();
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

        void DisableComponnetsWhenDestroied()
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        // What happens when a player collects the dot
        public virtual void BehaveWhenIteractWithPlayer()
        {
            DisableComponnetsWhenDestroied();
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

        public void ShowDestroyParticles(bool isGoodDot)
        {
            GameObject particleGO = Instantiate(particles.gameObject, this.transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleGO.GetComponent<ParticleSystem>();
            Color particlesColor = isGoodDot ? Color.green : Color.red;
            ParticleSystem.MainModule main = particleSystem.main;
            main.startColor = particlesColor;
            particleSystem.Play();
        }
    }
}