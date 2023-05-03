using UnityEngine;
using Dots.Utils.Interaction;

namespace Dots.GamePlay.Player.Interaction
{
    public class PlayerInteractionWithDots : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IInteractableObjects interactable))
            {
                interactable.BehaveWhenInteractWithPlayer();
            }
        }
    } 
}