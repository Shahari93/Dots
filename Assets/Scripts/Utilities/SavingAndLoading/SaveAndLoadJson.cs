using System.IO;
using UnityEngine;
using Dots.Coins.Model;
using Dots.Powerup.Model;
using Dots.GamePlay.Powerups;
using System.Runtime.Serialization.Formatters.Binary;

namespace Dots.Utilities.SaveAndLoad
{
    public class SaveAndLoadJson
    {
        public static void SavingToJson(string fileName, ISaveable saveable)
        {
            // Creating the file
            BinaryFormatter formatter = new();
            string path = Application.persistentDataPath + fileName;
            FileStream stream = new(path, FileMode.Create);

            // Saving the data to the model
            SaveableData data = new();
            data.userCoinsAmount = CoinsModel.CurrentCoinsAmount;
            // Closing the saved file
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static SaveableData LoadFromJson(string fileName)
        {
            string path = Application.persistentDataPath + fileName;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                FileStream stream = new(path, FileMode.Open);
                SaveableData data = formatter.Deserialize(stream) as SaveableData;
                CoinsModel.CurrentCoinsAmount = data.userCoinsAmount;
                stream.Close();
                return data;
            }
            else
            {
                return null;
            }
        }

        public static void SavePowerupValues(string fileName, ISaveable saveable, PowerupEffectSO powerup)
        {
            // Creating the file
            BinaryFormatter formatter = new();
            string path = Application.persistentDataPath + fileName;
            FileStream stream = new(path, FileMode.Create);

            // Saving the data to the model
            SaveableData data = new();
            powerup.powerupDuration = PowerupUpgradesModel.PowerupDurationValue;
            powerup.upgradeCoinsCost = PowerupUpgradesModel.CoinsCost;

            // Closing the saved file
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static SaveableData LoadPowerupValues(string fileName, PowerupEffectSO powerup)
        {
            string path = Application.persistentDataPath + fileName;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                FileStream stream = new(path, FileMode.Open);
                SaveableData data = formatter.Deserialize(stream) as SaveableData;
                PowerupUpgradesModel.PowerupDurationValue = powerup.powerupDuration;
                PowerupUpgradesModel.CoinsCost = powerup.upgradeCoinsCost;

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