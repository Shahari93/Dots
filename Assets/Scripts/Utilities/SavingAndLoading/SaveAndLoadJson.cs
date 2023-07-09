using System.IO;
using UnityEngine;
using Dots.Coins.Model;
using Dots.Powerup.Model;
using Dots.GamePlay.Powerups;
using System.Runtime.Serialization.Formatters.Binary;
using Dots.Feature.KeyAndChest.Key;
using Dots.Feature.KeyAndChest.Key.Model;

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
            data.totalCollectedKeys = KeysModel.TotalKeys;
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
                KeysModel.TotalKeys = data.totalCollectedKeys;
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

            if (fileName.Contains("Spawn Greens Powerup"))
            {
                data.spawnGreensDuration = powerup.powerupDuration;
                data.spawnGreensUpgradeCost = powerup.upgradeCoinsCost;
            }
            if (fileName.Contains("Slow Speed Powerup"))
            {
                data.slowTimeDuration = powerup.powerupDuration;
                data.slowTimeUpgradeCost = powerup.upgradeCoinsCost;
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
                if (fileName.Contains("Spawn Greens Powerup"))
                {
                    powerup.powerupDuration = data.spawnGreensDuration;
                    powerup.upgradeCoinsCost = data.spawnGreensUpgradeCost;
                    
                }
                if (fileName.Contains("Slow Speed Powerup"))
                {
                    powerup.powerupDuration = data.slowTimeDuration;
                    powerup.upgradeCoinsCost = data.slowTimeUpgradeCost;
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