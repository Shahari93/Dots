using UnityEngine;
using DG.Tweening;
using Dots.Utils.Spawnable;
using Dots.Utils.Interaction;
using System.Threading.Tasks;

namespace Dots.GamePlay.Dot
{
    public abstract class DotsBehaviour : MonoBehaviour, IInteractableObjects, ISpawnableObjects, IDestroyableObject
    {
        [SerializeField] protected Rigidbody2D rb2D;
        [SerializeField] protected ParticleSystem particles;

        float dotSpeed;
        float randX;
        float randY;
        Vector2 direction;
        protected Vector3 startScale = new Vector3(0.71f, 0.71f, 0f);

        public float Speed { get => dotSpeed; set => dotSpeed = value; }
        public float RandX { get => RandX; set => randX = value; }
        public float RandY { get => RandY; set => randY = value; }
        public Vector2 Direction { get => direction; set => direction = value; }
        protected bool IsGoodDot { get; set; }

        void OnEnable()
        {
            SetSpawnValues();
        }
        private async void SetSpawnValues()
        {
            dotSpeed = Random.Range(85f, 90f);
            randX = Random.Range(-180, 181);
            randY = Random.Range(-180, 181);
            direction = new Vector2(randX, randY).normalized;
            transform.localScale = Vector3.zero;
            await Task.Delay(500);
        }

        void FixedUpdate()
        {    
            transform.DOScale(0.7f, 0.5f).OnComplete(() =>
            {
                SetSpeedAndDirection();
            });
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
            transform.localScale -= startScale;
            DisablePowerupVisuals();
        }

        /// <summary>
        /// Abstract method to control what happens when a dot is hit by the player
        /// </summary>
        public abstract void BehaveWhenInteractWithPlayer();

        public void DisablePowerupVisuals()
        {
            ShowDestroyParticles(IsGoodDot);
            gameObject.SetActive(false);
        }

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