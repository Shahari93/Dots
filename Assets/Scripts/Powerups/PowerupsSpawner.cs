using UnityEngine;
using System.Collections;
using Dots.GamePlay.Powerups.Pool;

public class PowerupsSpawner : MonoBehaviour
{
    private static bool canSpawn = true;
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
    private void Start()
    {
        StartCoroutine(SpawnPowerups());
    }
    IEnumerator SpawnPowerups()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            float randomNumber = Random.Range(0f, 1f);
            string spawnableTag = "";


            if (canSpawn)
            {
                if (randomNumber >= 0.0f && randomNumber <= 0.2f)
                {
                    spawnableTag = "AllGreen";
                    canSpawn = false;
                }
                else if (randomNumber > 0.2f && randomNumber <= 0.4f)
                {
                    spawnableTag = "Shield";
                    canSpawn = false;
                }
                else if (randomNumber > 0.4f && randomNumber <= 1f)
                {
                    spawnableTag = "SlowSpeed";
                    canSpawn = false;
                }
            }


            GameObject spawnable = PowerupObjectPool.SharedInstance.PullObject(spawnableTag);
            if (spawnable != null)
            {
                spawnable.transform.position = Random.insideUnitCircle.normalized * 1.8f;
                spawnable.transform.rotation = this.transform.rotation;
                spawnable.GetComponent<Collider2D>().enabled = true;
                spawnable.GetComponent<SpriteRenderer>().enabled = true;
                spawnable.SetActive(true);
            }
        }
    }

    // Add a coroutine to disable the powerup after X seconds

}
