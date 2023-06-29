using System.IO;
using UnityEngine;
using Dots.Coins.Model;
using Dots.GamePlay.Powerups.Upgrade;
using System.Runtime.Serialization.Formatters.Binary;
using Dots.GamePlay.Powerups;
using Dots.Powerup.Model;
using Dots.Powerup.Upgrade;
using System.ComponentModel;

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

        public static void SavePowerupValues(string fileName, ISaveable saveable, PowerupEffectSO[] powerup)
        {
            // Creating the file
            BinaryFormatter formatter = new();
            string path = Application.persistentDataPath + fileName;
            FileStream stream = new(path, FileMode.Create);

            // Saving the data to the model
            SaveableData data = new();
            data.spawnGreensPowerupDurationData = PowerupUpgradesModel.PowerupDurationValue[0];
            data.spawnGreensUpgradeCoinsCostAmount = PowerupUpgradesModel.CoinsCost[0];

            data.slowTimePowerupDurationData = PowerupUpgradesModel.PowerupDurationValue[1];
            data.slowTimeUpgradeCoinsCostAmount = PowerupUpgradesModel.CoinsCost[1];
            // Closing the saved file
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static SaveableData LoadPowerupValues(string fileName, PowerupEffectSO[] powerup)
        {
            string path = Application.persistentDataPath + fileName;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new();
                FileStream stream = new(path, FileMode.Open);
                SaveableData data = formatter.Deserialize(stream) as SaveableData;
                PowerupUpgradesModel.PowerupDurationValue[0] = data.spawnGreensPowerupDurationData;
                PowerupUpgradesModel.CoinsCost[0] = data.spawnGreensUpgradeCoinsCostAmount;

                PowerupUpgradesModel.PowerupDurationValue[1] = data.slowTimePowerupDurationData;
                PowerupUpgradesModel.CoinsCost[1] = data.slowTimeUpgradeCoinsCostAmount;
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