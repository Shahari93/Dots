using UnityEngine;
using Dots.Utils.SaveAndLoad;
using Dots.ScorePoints.Model;

[System.Serializable]
public class CoinsData
{
    public int savedCoinsInJson;
}

namespace Dots.Coins.Model
{

    public class CoinsModel : MonoBehaviour
    {
        public static CoinsModel Instance;

        static int currentCoinsAmount;
        static int coinsToAdd;
        public static int CurrentCoinsAmount { get => currentCoinsAmount; set => currentCoinsAmount = value; }
        public static int CoinsToAdd { get => coinsToAdd; set => coinsToAdd = value; }

        private void OnEnable()
        {
            SaveAndLoadJson.LoadCoinsFromJson();
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
            }
            DontDestroyOnLoad(gameObject);
        }

        public void UpdateCoinsDataOnRv(int coins)
        {
            currentCoinsAmount += coins;
            SaveAndLoadJson.SaveCoinsToJson();
        }

        public void UpdateCoinsData()
        {
            coinsToAdd = PointsModel.CurrentPointsScore;
            if (coinsToAdd > 0)
            {
                currentCoinsAmount += coinsToAdd;
                SaveAndLoadJson.SaveCoinsToJson();
            }
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
                SaveAndLoadJson.SaveCoinsToJson();
            }
        }
    }
}