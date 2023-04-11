using UnityEngine;
using Dots.GamePlay.Dot;

public class PlayerDotsInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            collision.GetComponent<DotsBehaviour>().BehaveWhenIteractWithPlayer();
        }
    }
}
