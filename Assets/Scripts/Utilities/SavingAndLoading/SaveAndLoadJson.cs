using System.IO;
using UnityEngine;
using Dots.Coins.Model;
using Dots.GamePlay.Powerups.Upgrade;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dots.Utils.SaveAndLoad
{
    public class SaveAndLoadJson
    {
        public static void SavingToJson(string fileName, ISaveable saveable)
        {
            // Creating the file
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + fileName;
            FileStream stream = new FileStream(path, FileMode.Create);

            // Saving the data to the model
            SaveableData data = new SaveableData();
            data.userCoinsAmount = CoinsModel.CurrentCoinsAmount;
            data.upgradeCoinsCostAmount = UpgradePowerup.CoinsCost;
            data.powerupDurationData = UpgradePowerup.PowerupDurationValue;

            // Closing the saved file
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static SaveableData LoadFromJson(string fileName)
        {
            string path = Application.persistentDataPath + fileName;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                SaveableData data = formatter.Deserialize(stream) as SaveableData;
                CoinsModel.CurrentCoinsAmount = data.userCoinsAmount;
                UpgradePowerup.CoinsCost = data.upgradeCoinsCostAmount;
                UpgradePowerup.PowerupDurationValue = data.powerupDurationData;
                stream.Close();
                return data;
            }
            else
            {
                return null;
            }
        }
    }
}
#region RemoveAfterTetingOnAPK
/*public static void SaveCoinsToJson()
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
                return null;
            }
        }

        public static void SavePowerupDurationToJson()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/powerupDuration.json";
            FileStream stream = new FileStream(path, FileMode.Create);

            PowerupData data = new PowerupData();
            data.powerupDurationData = UpgradePowerup.PowerupDurationValue;
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static PowerupData LoadPowerupDurationFromJson()
        {
            string path = Application.persistentDataPath + "/powerupDuration.json";
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                PowerupData data = formatter.Deserialize(stream) as PowerupData;
                UpgradePowerup.PowerupDurationValue = data.powerupDurationData;
                stream.Close();
                return data;
            }
            else
            {
                return null;
            }
        }*/
#endregion