using UnityEngine;
using Dots.GamePlay.Dot;

public class PlayerDotsInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GoodDot") || collision.CompareTag("BadDot"))
        {
            collision.GetComponent<DotsBehaviour>().BehaveWhenIteractWithPlayer();
        }
    }
}
