using UnityEngine;
using Dots.Utilities.Destroy;
using System.Collections;
using Dots.GamePlay.Powerups;
using System.Collections.Generic;
using Dots.GamePlay.PowerupsParent.Pool;

namespace Dots.Utilities.Powerups.ObjectPool
{
    /// <summary>
    /// The object that spawn the powerups
    /// </summary>
    public class PowerupsSpawner : MonoBehaviour
    {
        // Interval between spawning 
        [Range(5f, 20f)][SerializeField] float powerupSpawnInterval;
        float powerupSpawnIntervalInitValue;

        [SerializeField] List<float> powerupsSpawnChancesList = new();
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
            powerupSpawnIntervalInitValue = powerupSpawnInterval;
            DestroyingPowerup.OnCollectedPower += StopPowerupsSpawn;
            DestroyingPowerup.OnPowerupDisabled += EnablePowerupSpawn;
        }
        /// <summary>
        /// If the powerup can be spawn we start the coroutine
        /// </summary>
        void EnablePowerupSpawn()
        {
            canSpawn = true;
            powerupSpawnInterval = powerupSpawnIntervalInitValue;
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
            //Adding each powerup spawn chance to the spawn chance list
            foreach (var powerupObject in powerupObjects)
            {
                powerupsSpawnChancesList.Add(powerupObject.spawnChance);
            }
            // Adding the total of spawn chances 
            foreach (var spawnChance in powerupsSpawnChancesList)
            {
                totalSpawnChance += spawnChance;
            }
        }

        void Update()
        {
            powerupSpawnInterval -= Time.deltaTime;
            Debug.Log("Can spawn" + canSpawn);
        }

        /// <summary>
        /// The coroutine that is responsible for spawning powerups 
        /// Checking the random number that was set and compare it to the spawn chance
        /// Pass the powerup name to the object pool function
        /// </summary>
        /// <returns>yielding for 3 seconds between each spawn</returns>
        IEnumerator SpawnPowerups()
        {
            while (canSpawn)
            {
                yield return new WaitForSeconds(3f);
                float randomNumber = Random.Range(0f, totalSpawnChance);
                string spawnableName = "";


                if (canSpawn && Time.deltaTime >= powerupSpawnInterval)
                {
                    for (int i = 0; i < powerupsSpawnChancesList.Count; i++)
                    {
                        if (randomNumber <= powerupsSpawnChancesList[i])
                        {
                            spawnableName = powerupObjects[i].name;
                        }
                        else
                        {
                            randomNumber -= powerupsSpawnChancesList[i];
                        }
                    }
                    powerupSpawnInterval = powerupSpawnIntervalInitValue;

                }


                GameObject spawnable = PowerupObjectPool.SharedInstance.PullObject(spawnableName);
                if (spawnable != null)
                {
                    spawnable.transform.SetPositionAndRotation(this.transform.position, this.transform.rotation);
                    spawnable.GetComponent<Collider2D>().enabled = true;
                    spawnable.GetComponent<SpriteRenderer>().enabled = true;
                    spawnable.SetActive(true);
                }
            }
        }
        void OnDisable()
        {
            DestroyingPowerup.OnCollectedPower -= StopPowerupsSpawn;
            DestroyingPowerup.OnPowerupDisabled -= EnablePowerupSpawn;
        }
    }
}