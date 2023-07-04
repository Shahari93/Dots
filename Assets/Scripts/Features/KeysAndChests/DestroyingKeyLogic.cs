using System;
using UnityEngine;
using Dots.Utilities.SaveAndLoad;
using Dots.Utilities.Interface.Destroy;
using Dots.Utilities.Interface.Interaction;

namespace Dots.Feature.KeyAndChest.Key
{
    public class DestroyingKeyLogic : MonoBehaviour, IInteractableObjects, IDestroyableObject, ISaveable
    {
        [SerializeField] ParticleSystem particles;

        public static event Action<int> OnKeyCollected;

        static int totalKeys;

        public static int TotalKeys { get => totalKeys; set => totalKeys = value; }

        void OnEnable()
        {
            SaveAndLoadJson.LoadFromJson("/SavedData.json");
        }

        public void BehaveWhenInteractWithBounds()
        {
            // Need to delete this somehow because of how we spawn the keys
        }

        public void BehaveWhenInteractWithPlayer()
        {
            totalKeys++;
            OnKeyCollected?.Invoke(totalKeys);
            DisableVisuals();
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
        private void OnDisable()
        {
            SaveAndLoadJson.SavingToJson("/SavedData.json", this);
        }
    }
}