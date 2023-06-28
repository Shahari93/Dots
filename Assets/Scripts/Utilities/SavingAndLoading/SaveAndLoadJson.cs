using System.IO;
using UnityEngine;
using Dots.Coins.Model;
using Dots.GamePlay.Powerups.Upgrade;
using System.Runtime.Serialization.Formatters.Binary;
using Dots.GamePlay.Powerups;
using Dots.Powerup.Model;

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
            for (int i = 0; i < data.upgradeCoinsCostAmount.Length; i++)
            {
                data.upgradeCoinsCostAmount[i] = PowerupUpgradesModel.CoinsCost[i];
                data.powerupDurationData[i] = PowerupUpgradesModel.PowerupDurationValue[i];
            }
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
                for (int i = 0; i < data.upgradeCoinsCostAmount.Length; i++)
                {
                    PowerupUpgradesModel.CoinsCost[i] = data.upgradeCoinsCostAmount[i];
                    PowerupUpgradesModel.PowerupDurationValue[i] = data.powerupDurationData[i];
                }
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