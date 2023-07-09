using Dots.Utilities.SaveAndLoad;
using UnityEngine;

namespace Dots.Feature.KeyAndChest.Key.Model
{
    public class KeysModel : MonoBehaviour, ISaveable
    {
        public static KeysModel Instance;

        static int totalKeys;

        public static int TotalKeys { get => totalKeys; set => totalKeys = value; }

        void OnEnable()
        {
            SaveAndLoadJson.LoadFromJson("/SavedData.json");
        }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void IncrementKeysValue()
        {
            totalKeys++;
        }

        public void DecreaseKeysValue()
        {
            totalKeys--;
        }

        void OnDisable()
        {
            SaveAndLoadJson.SavingToJson("/SavedData.json", this);
        }
    }
}