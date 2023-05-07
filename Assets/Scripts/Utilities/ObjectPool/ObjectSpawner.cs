using UnityEngine;
using System.Collections;
using Dots.GamePlay.Dot.Bad;
using Dots.GamePlay.Dot.Good;
using Dots.GamePlay.Dot.Timer;

namespace Dots.Utils.ObjectPool
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] float spawnTime;

        [SerializeField] float[] spawnChances;
        [SerializeField] GameObject[] dotObjects;
        float total;
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
            spawnChances = new float[2] { BadDot.spawnChance, GoodDot.spawnChance };
            foreach (var spawnChance in spawnChances)
            {
                total += spawnChance;
            }
        }

        // Testing changing the spawn percentage
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GoodDot.spawnChance = 1f;
                if (spawnChances[1] != GoodDot.spawnChance)
                {
                    spawnChances[1] = GoodDot.spawnChance;
                    return;
                }
            }
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(spawnTime);
                float randomNumber = Random.Range(0f, total);
                string spawnableTag = "";



                for (int i = 0; i < spawnChances.Length; i++)
                {
                    if (randomNumber <= spawnChances[i])
                    {
                        spawnableTag = dotObjects[i].tag;
                    }
                    else
                    {
                        randomNumber -= spawnChances[i];
                    }
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