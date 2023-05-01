using UnityEngine;
using System.Collections;
using System.Threading.Tasks;
using Dots.GamePlay.Dot.Timer;

namespace Dots.Utils.ObjectPool
{
    public class SpawnDots : MonoBehaviour
    {
        float spawnTime;

        void OnEnable()
        {
            BadDot.OnLoseGame += StopSpawnInvokation;
            IncreaseSpeedOverTime.OnTenSecondsPassed += ChangeSpeed;
        }

        private void Awake()
        {
            spawnTime = 1.5f;
        }

        void Start()
        {
            StartCoroutine(Spawn());
        }

        private void Update()
        {
            Debug.Log(spawnTime);
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(spawnTime);
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

        private void ChangeSpeed(int ticks)
        {
            if (ticks > 0)
            {
                spawnTime -= 0.1f;
                return;
            }

            if (spawnTime <= 0.5f)
            {
                spawnTime = 0.5f;
            }
        }

        async void StopSpawnInvokation()
        {
            await Task.Delay(50);
            StopCoroutine(Spawn());
        }

        void OnDisable()
        {
            BadDot.OnLoseGame -= StopSpawnInvokation;
        }
    }
}