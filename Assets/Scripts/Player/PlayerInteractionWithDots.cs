using UnityEngine;
using Dots.GamePlay.Dot;
using Dots.Utils.Interaction;

public class PlayerInteractionWithDots : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractableObjects interactable))
        {
            interactable.BehaveWhenIteractWithPlayer();
        }
    }
}
