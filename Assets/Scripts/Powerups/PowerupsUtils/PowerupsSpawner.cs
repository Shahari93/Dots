using UnityEngine;
using System.Collections;
using Dots.GamePlay.PowerupsPerent.Pool;

namespace Dots.Utils.Powerups.Objectpool
{
    public class PowerupsSpawner : MonoBehaviour
    {
        [Range(5f, 20f)][SerializeField] float powerupSpawnIntirval;
        float powerupSpawnIntirvalInitValue;

        static bool canSpawn;
        public static bool CanSpawn
        {
            get
            {
                return canSpawn;
            }
            set
            {
                canSpawn = value;
            }
        }

        void OnEnable()
        {
            powerupSpawnIntirvalInitValue = powerupSpawnIntirval;
        }

        void Start()
        {
            canSpawn = true;
            StartCoroutine(SpawnPowerups());
        }

        void Update()
        {
            powerupSpawnIntirval -= Time.deltaTime;
        }

        IEnumerator SpawnPowerups()
        {
            while (true)
            {
                yield return new WaitForSecondsRealtime(3f);
                float randomNumber = Random.Range(0f, 1f);
                string spawnableTag = "";


                if (canSpawn && Time.deltaTime >= powerupSpawnIntirval)
                {
                    if (randomNumber >= 0.0f && randomNumber <= 0.4f)
                    {
                        spawnableTag = "AllGreen";
                        canSpawn = false;
                    }
                    else if (randomNumber > 0.4f && randomNumber <= 1)
                    {
                        spawnableTag = "Shield";
                        canSpawn = false;
                    }
                    powerupSpawnIntirval = powerupSpawnIntirvalInitValue;

                }


                GameObject spawnable = PowerupObjectPool.SharedInstance.PullObject(spawnableTag);
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
    }
}