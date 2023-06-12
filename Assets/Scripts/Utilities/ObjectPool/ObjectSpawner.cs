using UnityEngine;
using Dots.Utils.FTUE;
using Dots.Utils.Destroy;
using System.Collections;
using Dots.GamePlay.Dot.Bad;
using Dots.GamePlay.Dot.Good;
using Dots.GamePlay.Dot.Timer;
using Dots.Audio.Manager;

namespace Dots.Utils.ObjectPool
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] float spawnTime;

        [SerializeField] float[] spawnChances;
        [SerializeField] GameObject[] dotObjects;
        float total;
        bool ftueSpawn = true;

        void OnEnable()
        {
            BadDot.OnLoseGame += StopSpawnInvokation;
            IncreaseSpeedOverTime.OnTickIncreased += ChangeSpawnSpeed;
            DestroingPowerup.OnCollectedPower += StartDisablePowerupCoroutine;
        }

        void Awake()
        {
            spawnTime = 1.5f;
            GoodDot.spawnChance = 0.15f;
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
        void Update()
        {
            if (spawnChances[1] != GoodDot.spawnChance)
            {
                spawnChances[1] = GoodDot.spawnChance;
                return;
            }
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnTime);
                float randomNumber = Random.Range(0f, total);
                string spawnableTag = "";

                if(PlayerPrefs.HasKey("LaunchCount") && CheckForFTUE.LaunchCount < 1 && ftueSpawn)
                {
                    GoodDot.spawnChance = 1f;
                    StartCoroutine(DisablePowerupAbility(8f));
                    ftueSpawn = false;
                }

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
                    spawnable.transform.position = transform.position;
                    spawnable.transform.rotation = transform.rotation;
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

        void StartDisablePowerupCoroutine(float duration)
        {
            StartCoroutine(DisablePowerupAbility(duration));
        }

        public IEnumerator DisablePowerupAbility(float duration)
        {
            if (duration <= 0)
            {
                yield break;
            }

            while (duration > 0)
            {
                duration -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
                if (duration <= 0)
                {
                    GoodDot.spawnChance = 0.15f;
                    AudioManager.Instance.PlaySFX("PowerupDisabled");
                    DestroingPowerup.OnPowerupDisabled?.Invoke();
                    break;
                }
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
            DestroingPowerup.OnCollectedPower -= StartDisablePowerupCoroutine;
        }
    }
}