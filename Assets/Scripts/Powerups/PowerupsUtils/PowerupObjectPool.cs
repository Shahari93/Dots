using UnityEngine;
using System.Collections.Generic;

namespace Dots.GamePlay.PowerupsParent.Pool 
{
    /// <summary>
    /// Object pool class for powerups
    /// </summary>
    public class PowerupObjectPool : MonoBehaviour
    {
        public static PowerupObjectPool SharedInstance;

        [SerializeField] GameObject[] powerupsObjects;
        [SerializeField] List<GameObject>[] powerupsPooledObjects;
        [SerializeField] int[] amountToBuffer;
        [SerializeField] int defaultAmount = 3;

        void Awake()
        {
            SharedInstance = this;
        }

        void Start()
        {
            InitObjectPoolList();
        }

        /// <summary>
        /// Initializing the object pool with objects 
        /// with the amount of object to pool we set in the inspector at the start of the game
        /// </summary>
        void InitObjectPoolList()
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

        /// <summary> 
        /// Pull an object of a specific type based on it's name
        /// from the object pool.
        /// </summary>
        /// <param name="objectType">the pooled object name we pass</param>
        /// <returns></returns>
        public GameObject PullObject(string objectType)
        {
            bool onlyPooled = false;
            for (int i = 0; i < powerupsObjects.Length; i++)
            {
                GameObject prefab = powerupsObjects[i];
                if (prefab.name == objectType)
                {
                    if (powerupsPooledObjects[i].Count > 0)
                    {
                        GameObject pooledObject = powerupsPooledObjects[i][0];
                        pooledObject.SetActive(true);
                        pooledObject.transform.parent = this.transform;
                        powerupsPooledObjects[i].Remove(pooledObject);
                        return pooledObject;
                    }
                    else if (!onlyPooled)
                    {
                        return Instantiate(powerupsObjects[i], this.transform);
                    }
                    break;
                }
            }
            // Null if there's a hit miss.
            return null;
        }

        /// <summary>
        /// Add object of a specific type to the pool. 
        /// </summary>
        /// <param name="obj">The object to add</param>
        public void PoolObject(GameObject obj)
        {
            for (int i = 0; i < powerupsObjects.Length; i++)
            {
                if (obj.CompareTag(powerupsObjects[i].tag))
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