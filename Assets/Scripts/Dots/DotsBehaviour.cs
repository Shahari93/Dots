using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Dot
{
    public abstract class DotsBehaviour : MonoBehaviour, IInteractableObjects
    {
        [SerializeField] float speed;
        [SerializeField] Vector2 direction;

        public bool IsGoodDot { get; private set; }

        public virtual void BehaveWhenIteract()
        {
            
            Destroy(gameObject);
        }
    }
}