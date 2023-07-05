using UnityEngine;
using Dots.Utilities.SaveAndLoad;
using Dots.ScorePoints.Model;
using Dots.Feature.KeyAndChest.Chest.Tap;

[System.Serializable]
public class CoinsData
{
    public int savedCoinsInJson;
}

namespace Dots.Coins.Model
{
    public class CoinsModel : MonoBehaviour, ISaveable
    {
        public static CoinsModel Instance;

        static int currentCoinsAmount/* = 2000*/;
        static int coinsToAdd;
        public static int CurrentCoinsAmount { get => currentCoinsAmount; set => currentCoinsAmount = value; }
        public static int CoinsToAdd { get => coinsToAdd; set => coinsToAdd = value; }

        void OnEnable()
        {
            SaveAndLoadJson.LoadFromJson("/SavedData.json");
        }

        void Awake()
        {
            Debug.Log(Application.persistentDataPath);
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

        //TODO: Test on APK that the added coins from RV are saving to the JSON
        public void UpdateCoinsDataOnRv(int coins)
        {
            currentCoinsAmount += coins;
            SaveAndLoadJson.SavingToJson("/SavedData.json", this);
        }

        public void UpdateCoinsData()
        {
            coinsToAdd = PointsModel.CurrentPointsScore;
            if (coinsToAdd > 0)
            {
                currentCoinsAmount += coinsToAdd;
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);
            }
        }

        public void UpdateCoinsDataFromChest()
        {
            coinsToAdd += TapOnChest.TotalCoinsFromChests + PointsModel.CurrentPointsScore;
            currentCoinsAmount += coinsToAdd;
            SaveAndLoadJson.SavingToJson("/SavedData.json", this);
        }

        public void ResetCoins()
        {
            coinsToAdd = 0;
        }

        public void UpdateCoinsDataAfterUpgrade(int coinCost)
        {
            if (currentCoinsAmount > 0)
            {
                currentCoinsAmount -= coinCost;
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);
            }
        }
    }
}