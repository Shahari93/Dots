using UnityEngine;
using System.Collections;
using Dots.GamePlay.Dot.Bad;
using System.Threading.Tasks;
using Dots.GamePlay.Dot.Timer;

namespace Dots.Utils.ObjectPool
{
    public class ObjectSpawner : MonoBehaviour
    {
        float spawnTime;

        void OnEnable()
        {
            BadDot.OnLoseGame += StopSpawnInvokation;
            IncreaseSpeedOverTime.OnTickIncreased += ChangeSpawnSpeed;
        }

        void Awake()
        {
            spawnTime = 1.5f;
        }

        void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(spawnTime);
                float randomNumber = Random.Range(0f, 1f);
                string spawnableTag = "";

                if (randomNumber >= 0.0f && randomNumber <= 0.1f)
                {
                    spawnableTag = "GoodDot";
                }
                else if (randomNumber > 0.1f && randomNumber <= 0.85f)
                {
                    spawnableTag = "BadDot";
                }
                else if (randomNumber > 0.85f && randomNumber <= 0.88f)
                {
                    spawnableTag = "AllGreen";
                }
                else if (randomNumber > 0.88f && randomNumber <= 0.93f)
                {
                    spawnableTag = "Shield";
                }
                else if (randomNumber > 0.93f && randomNumber <= 1f)
                {
                    spawnableTag = "SlowSpeed";
                }

                GameObject spawnable = ObjectPooler.SharedInstance.GetPooledObject(spawnableTag);
                if (spawnable != null)
                {
                    spawnable.transform.position = this.transform.position;
                    spawnable.transform.rotation = this.transform.rotation;
                    spawnable.GetComponent<Collider2D>().enabled = true;
                    spawnable.GetComponent<SpriteRenderer>().enabled = true;
                    spawnable.SetActive(true);
                }
            }
        }

        void ChangeSpawnSpeed(int ticks)
        {
            if (ticks > 0)
            {
                spawnTime -= 0.1f;
            }

            if (spawnTime <= 0.5f)
            {
                spawnTime = 0.5f;
            }
        }

        void StopSpawnInvokation()
        {
            StopCoroutine(Spawn());
        }

        void OnDisable()
        {
            BadDot.OnLoseGame -= StopSpawnInvokation;
            IncreaseSpeedOverTime.OnTickIncreased -= ChangeSpawnSpeed;
        }
    }
}