using UnityEngine;
using System.Threading.Tasks;
using Dots.Utilities.Spawn.CircleCircumference;
using Dots.Feature.KeyAndChest.Key.Display;

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
        }

        async Task AsyncSpawnKey()
        {
            await Task.Delay(Mathf.RoundToInt(randomSpawnInterval) * 1000);
            if (Time.timeSinceLevelLoad >= randomSpawnInterval && timesKeySpawned == 0 && DestroyingKeyLogic.TotalKeys < 3)
            {
                GameObject newKey = Instantiate(keyToSpawn.gameObject, SpawnOnCircleCircumference.SpawnObjectOnCircleCircumference(1.8f), Quaternion.identity);
                timesKeySpawned++;
                return;
            }

            if (DestroyingKeyLogic.TotalKeys >= 3)
            {
                return;
            }

            else
            {
                await AsyncSpawnKey();
            }
        }
    }
}