using UnityEngine;
using System.Collections.Generic;

namespace Dots.GamePlay.Powerups.Pool 
{
    public class PowerupObjectPool : MonoBehaviour
    {
        public static PowerupObjectPool SharedInstance;

        [SerializeField] GameObject[] powerupsObjects;
        [SerializeField] List<GameObject>[] powerupsPooledObjects;
        [SerializeField] int[] amountToBuffer;
        [SerializeField] int defaultAmount = 3;


        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            powerupsPooledObjects = new List<GameObject>[powerupsObjects.Length];
            int i = 0;
            foreach (GameObject obj in powerupsObjects)
            {
                powerupsPooledObjects[i] = new List<GameObject>();
                int bufferAmount;
                if (i < amountToBuffer.Length)
                {
                    bufferAmount = amountToBuffer[i];
                }
                else
                {
                    bufferAmount = defaultAmount;
                }
                for (int n = 0; n < bufferAmount; n++)
                {
                    GameObject newObj = Instantiate(obj);
                    newObj.name = obj.name;
                    PoolObject(newObj);
                }
                i++;
            }
        }

        // Pull an object of a specific type from the pool.
        public GameObject PullObject(string objectType)
        {
            bool onlyPooled = false;
            for (int i = 0; i < powerupsObjects.Length; i++)
            {
                GameObject prefab = powerupsObjects[i];
                if (prefab.tag == objectType)
                {
                    if (powerupsPooledObjects[i].Count > 0)
                    {
                        GameObject pooledObject = powerupsPooledObjects[i][0];
                        pooledObject.SetActive(true);
                        pooledObject.transform.parent = null;
                        powerupsPooledObjects[i].Remove(pooledObject);
                        return pooledObject;
                    }
                    else if (!onlyPooled)
                    {
                        return Instantiate(powerupsObjects[i]);
                    }
                    break;
                }
            }
            // Null if there's a hit miss.
            return null;
        }

        // Add object of a specific type to the pool.
        public void PoolObject(GameObject obj)
        {
            for (int i = 0; i < powerupsObjects.Length; i++)
            {
                if (powerupsObjects[i].tag == obj.tag)
                {
                    obj.SetActive(false);
                    obj.transform.parent = this.transform;
                    powerupsPooledObjects[i].Add(obj);
                    return;
                }
            }
            Destroy(obj);
        }
    }
}