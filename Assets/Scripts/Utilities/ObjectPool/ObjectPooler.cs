using UnityEngine;
using System.Collections.Generic;
using Dots.GamePlay.Dot;

namespace Dots.Utils.ObjectPool
{
    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int amountToPool;

        public bool shouldExpend = true;
    }

    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler SharedInstance;

        public List<GameObject> pooledObject;
        public List<ObjectPoolItem> itemsToPool;

        void Awake()
        {
            SharedInstance = this;
        }

        void Start()
        {
            InitObjectPool();
        }

        void InitObjectPool()
        {
            pooledObject = new List<GameObject>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = Instantiate(item.objectToPool, this.transform);
                    obj.SetActive(false);
                    pooledObject.Add(obj);
                }
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            for (int i = 0; i < pooledObject.Count; i++)
            {
                if (!pooledObject[i].gameObject.activeInHierarchy && pooledObject[i].tag == tag)
                {
                    return pooledObject[i].gameObject;
                }
                foreach (ObjectPoolItem item in itemsToPool)
                {
                    if (item.objectToPool.tag == tag)
                    {
                        if (item.shouldExpend)
                        {
                            GameObject obj = Instantiate(item.objectToPool, this.transform);
                            obj.SetActive(false);
                            pooledObject.Add(obj);
                            return obj;
                        }
                    }
                }
            }
            return null;
        }
    }
}