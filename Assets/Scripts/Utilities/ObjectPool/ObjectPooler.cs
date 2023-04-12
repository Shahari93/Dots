using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
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

        public List<DotsBehaviour> pooledObject;
        public List<ObjectPoolItem> itemsToPool;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            InitObjectPool();
        }

        private void InitObjectPool()
        {
            pooledObject = new List<DotsBehaviour>();
            foreach (ObjectPoolItem item in itemsToPool)
            {
                for (int i = 0; i < item.amountToPool; i++)
                {
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, this.transform);
                    obj.SetActive(false);
                    pooledObject.Add(obj.GetComponent<DotsBehaviour>());
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
                            GameObject obj = (GameObject)Instantiate(item.objectToPool, this.transform);
                            obj.SetActive(false);
                            pooledObject.Add(obj.GetComponent<DotsBehaviour>());
                            return obj;
                        }
                    }
                }
            }
            return null;
        }
    }
}