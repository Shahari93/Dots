using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Dots.Coins.Model;
using Dots.GamePlay.Powerups.Upgrade;

namespace Dots.Utils.SaveAndLoad
{
    public class SaveAndLoadJson
    {
        public static void SaveCoinsToJson()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/coins.json";
            FileStream stream = new FileStream(path, FileMode.Create);

            CoinsData data = new CoinsData();
            data.savedCoinsInJson = CoinsModel.CurrentCoinsAmount;
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static CoinsData LoadCoinsFromJson()
        {
            string path = Application.persistentDataPath + "/coins.json";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                CoinsData data = formatter.Deserialize(stream) as CoinsData;
                CoinsModel.CurrentCoinsAmount = data.savedCoinsInJson;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("file not found");
                return null;
            }
        }

        public static void SaveCoinsUpgradeToJson()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/coinsCost.json";
            FileStream stream = new FileStream(path, FileMode.Create);

            CoinsCostData data = new CoinsCostData();
            data.savedCoinsCostInJson = UpgradePowerup.CoinsCost;
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static CoinsCostData LoadCoinsUpgradeFromJson()
        {
            string path = Application.persistentDataPath + "/coinsCost.json";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                CoinsCostData data = formatter.Deserialize(stream) as CoinsCostData;
                UpgradePowerup.CoinsCost = data.savedCoinsCostInJson;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("file not found");
                return null;
            }
        }
    }
}