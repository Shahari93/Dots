using UnityEngine;
using Dots.Utilities.Interface.Interaction;

namespace Dots.GamePlay.Player.Interaction
{
    /// <summary>
    /// This class is checking with which interactable object collided with 
    /// and acting accordingly
    /// I.E the player collided with bad dot - we end the game
    /// </summary>
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