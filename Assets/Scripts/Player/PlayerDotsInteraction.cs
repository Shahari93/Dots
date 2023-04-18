using UnityEngine;
using Dots.GamePlay.Dot;

public class PlayerDotsInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoodDot"))
        {
            collision.GetComponent<DotsBehaviour>().BehaveWhenIteractWithPlayer();

            // Invoke a good dot event
        }

        else if (collision.CompareTag("BadDot"))
        {
            collision.GetComponent<DotsBehaviour>().BehaveWhenIteractWithPlayer();

            // Invoke a bad dot event
        }
    }
}
