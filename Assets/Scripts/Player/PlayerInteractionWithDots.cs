using UnityEngine;
using Dots.GamePlay.Dot;

public class PlayerInteractionWithDots : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoodDot") || collision.CompareTag("BadDot"))
        {
            collision.GetComponent<DotsBehaviour>().BehaveWhenIteractWithPlayer();
        }
    }
}
