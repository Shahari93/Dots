using UnityEngine;
using Dots.Utilities.Interface.Destroy;
using Dots.Utilities.Interface.Interaction;

namespace Dots.Feature.KeyAndChest.Key
{
    public class CollectingKey : MonoBehaviour, IInteractableObjects, IDestroyableObject
    {
        [SerializeField] ParticleSystem particles;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bounds"))
            {
                BehaveWhenInteractWithBounds();
            }
        }

        public void BehaveWhenInteractWithBounds()
        {
            DisableVisuals();
        }

        public void BehaveWhenInteractWithPlayer()
        {
            throw new System.NotImplementedException();
        }

        public void DisableVisuals()
        {
            ShowDestroyParticles(null);
            gameObject.SetActive(false);
        }

        public void ShowDestroyParticles(bool? isGoodDot = null)
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