using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Dots.Coins.Model;

namespace Dots.Utils.SaveAndLoad
{
    public class SaveAndLoadJson
    {
        public static void SaveToJson()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/coins.json";
            FileStream stream = new FileStream(path, FileMode.Create);

            CoinsData data = new CoinsData();
            data.savedCoinsInJson = CoinsModel.CurrentCoinsAmount;
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static CoinsData LoadFromJson()
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

        //public static void SaveCoinsToJson()
        //{
        //    if (!File.Exists(Application.persistentDataPath + "/CoinsValue.json"))
        //    {
        //        File.Create(Application.persistentDataPath + "/CoinsValue.json");
        //    }
        //    else
        //    {
        //        CoinsData data = new CoinsData();
        //        data.savedCoinsInJson = CoinsModel.CurrentCoinsAmount;
        //        string json = JsonUtility.ToJson(data, true);
        //        File.WriteAllText(Application.persistentDataPath + "/CoinsValue.json", json);
        //    }
        //}

        //public static void LoadCoinsFromJson()
        //{
        //    if (!File.Exists(Application.persistentDataPath + "/CoinsValue.json"))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        string Json = File.ReadAllText(Application.persistentDataPath + "/CoinsValue.json");
        //        CoinsData Data = JsonUtility.FromJson<CoinsData>(Json);
        //        CoinsModel.CurrentCoinsAmount = Data.savedCoinsInJson;
        //    }
        //}
    }
}