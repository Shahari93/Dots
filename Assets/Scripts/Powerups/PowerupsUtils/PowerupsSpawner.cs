using UnityEngine;
using Dots.Utils.Destroy;
using System.Collections;
using Dots.GamePlay.PowerupsPerent.Pool;
using Dots.GamePlay.Powerups;

namespace Dots.Utils.Powerups.Objectpool
{
    public class PowerupsSpawner : MonoBehaviour
    {
        // Interval between spawning 
        [Range(5f, 20f)][SerializeField] float powerupSpawnIntirval;
        float powerupSpawnIntirvalInitValue;

        [SerializeField] float[] powerupsSpawnChances;
        [SerializeField] PowerupEffectSO[] powerupObjects;
        float totalSpawnChance;

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
            DestroingPowerup.OnCollectedPower += StopPowerupsSpawn;
            DestroingPowerup.OnPowerupDisabled += EnablePowerupSpawn;
        }

        void EnablePowerupSpawn()
        {
            canSpawn = true;
            powerupSpawnIntirval = powerupSpawnIntirvalInitValue;
            StartCoroutine(SpawnPowerups());
        }

        void StopPowerupsSpawn(float obj)
        {
            canSpawn = false;
        }

        void Start()
        {
            canSpawn = true;
            StartCoroutine(SpawnPowerups());
            powerupsSpawnChances = new float[] { powerupObjects[0].spawnChance, powerupObjects[1].spawnChance };
            foreach (var spawnChance in powerupsSpawnChances)
            {
                totalSpawnChance += spawnChance;
            }
        }

        void Update()
        {
            powerupSpawnIntirval -= Time.deltaTime;
        }

        IEnumerator SpawnPowerups()
        {
            while (canSpawn)
            {
                yield return new WaitForSeconds(3f);
                float randomNumber = Random.Range(0f, totalSpawnChance);
                string spawnableTag = "";


                if (canSpawn && Time.deltaTime >= powerupSpawnIntirval)
                {
                    for (int i = 0; i < powerupsSpawnChances.Length; i++)
                    {
                        if (randomNumber <= powerupsSpawnChances[i])
                        {
                            spawnableTag = powerupObjects[i].name;
                        }
                        else
                        {
                            randomNumber -= powerupsSpawnChances[i];
                        }
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
        void OnDisable()
        {
            DestroingPowerup.OnCollectedPower -= StopPowerupsSpawn;
            DestroingPowerup.OnPowerupDisabled -= EnablePowerupSpawn;
        }
    }
}