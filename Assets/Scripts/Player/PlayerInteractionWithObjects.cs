using UnityEngine;
using Dots.Utils.Interface.Interaction;

namespace Dots.GamePlay.Player.Interaction
{
    public class PlayerInteractionWithObjects : MonoBehaviour
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