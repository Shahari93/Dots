using Dots.Coins.Model;
using System.IO;
using UnityEngine;

namespace Dots.Utils.SaveAndLoad
{
	public class SaveAndLoadJson
	{
		public static void SaveCoinsToJson()
		{
			CoinsData data = new CoinsData();
			data.savedCoinsInJson = CoinsModel.CurrentCoinsAmount;
			string json = JsonUtility.ToJson(data,true);
			File.WriteAllText(Application.dataPath + "/CoinsValue.json", json);
		}

		public static void LoadCoinsFromJson() 
		{
			if (!File.Exists(Application.dataPath + "/CoinsValue.json"))
				return;

			string json = File.ReadAllText(Application.dataPath + "/CoinsValue.json");
			CoinsData data = JsonUtility.FromJson<CoinsData>(json);
            CoinsModel.CurrentCoinsAmount = data.savedCoinsInJson;
		}
	} 
}