using UnityEngine;

namespace Dots.Utils.ObjectPool
{
    public class SpawnDots : MonoBehaviour
    {
        private void Start()
        {
            InvokeRepeating(nameof(Spawn), 2f, 1.5f);
        }

        private void Spawn()
        {
            int rand = Random.Range(1, 3);
            string dotTag = "";

            dotTag = rand == 1 ? "GoodDot" : "BadDot";

            GameObject dot = ObjectPooler.SharedInstance.GetPooledObject(dotTag);
            if (dot != null)
            {
                dot.transform.position = this.transform.position;
                dot.transform.rotation = this.transform.rotation;
                dot.GetComponent<Collider2D>().enabled = true;
                dot.GetComponent<SpriteRenderer>().enabled = true;
                dot.SetActive(true);
            }
        }
    }
}