using Dots.Utilities.Spawn.CircleCircumference;
using System.Threading.Tasks;
using UnityEngine;

namespace Dots.Feature.KeyAndChest.Key.Spawn
{
    public class KeySpawner : MonoBehaviour
    {
        [SerializeField] DestroyingKeyLogic keyToSpawn;
        int timesKeySpawned = 0;
        float randomSpawnInterval;

        async void Start()
        {
            randomSpawnInterval = Random.Range(5f, 15f);

            await AsyncSpawnKey();

            // Check how much passes since scene is loaded and if timesKeySpawned is 0 (Or somehow less then 0)
            // If time has passed the random interval
            // Spawn key and add 1 to timesKeySpawned

            // Use async method to delay the spawning
        }

        async Task AsyncSpawnKey()
        {
            await Task.Delay(Mathf.RoundToInt(randomSpawnInterval) * 1000);
            if (Time.timeSinceLevelLoad >= randomSpawnInterval && timesKeySpawned == 0)
            {
                GameObject newKey = Instantiate(keyToSpawn.gameObject, SpawnOnCircleCircumference.SpawnObjectOnCircleCircumference(1.8f), Quaternion.identity);
                timesKeySpawned++;
            }
            else
            {
                await AsyncSpawnKey();
            }
        }
    }
}