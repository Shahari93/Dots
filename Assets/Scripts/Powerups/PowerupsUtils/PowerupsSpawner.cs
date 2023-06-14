using UnityEngine;
using Dots.Utils.Destroy;
using System.Collections;
using Dots.GamePlay.Powerups;
using System.Collections.Generic;
using Dots.GamePlay.PowerupsPerent.Pool;

namespace Dots.Utils.Powerups.Objectpool
{
    public class PowerupsSpawner : MonoBehaviour
    {
        // Interval between spawning 
        [Range(5f, 20f)][SerializeField] float powerupSpawnIntirval;
        float powerupSpawnIntirvalInitValue;

        [SerializeField] List<float> powerupsSpawnChancesList = new List<float>();
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
            foreach (var powerupObject in powerupObjects)
            {
                powerupsSpawnChancesList.Add(powerupObject.spawnChance);
            }

            foreach (var spawnChance in powerupsSpawnChancesList)
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
                    for (int i = 0; i < powerupsSpawnChancesList.Count; i++)
                    {
                        if (randomNumber <= powerupsSpawnChancesList[i])
                        {
                            spawnableTag = powerupObjects[i].name;
                        }
                        else
                        {
                            randomNumber -= powerupsSpawnChancesList[i];
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