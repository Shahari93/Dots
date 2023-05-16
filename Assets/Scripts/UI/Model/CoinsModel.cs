using UnityEngine;
using Dots.Utils.SaveAndLoad;

[System.Serializable]
public class CoinsData
{
    public int savedCoinsInJson;
}

namespace Dots.Coins.Model
{

    public class CoinsModel : MonoBehaviour
    {
        static CoinsModel Instance;

        static int currentCoinsAmount;
        public static int CurrentCoinsAmount
        {
            get
            {
                return currentCoinsAmount;
            }
            set
            {
                currentCoinsAmount = value;
            }
        }

        private void OnEnable()
        {
            SaveAndLoadJson.LoadCoinsFromJson(currentCoinsAmount);
        }

        private void Awake()
        {
            if(Instance != null)
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

        private void UpdateCoins()
        {
            SaveAndLoadJson.SaveCoinsToJson(currentCoinsAmount);
        }
    }
}