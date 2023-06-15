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