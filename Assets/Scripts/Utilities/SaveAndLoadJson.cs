using System.IO;
using UnityEngine;

namespace Dots.Utils.SaveAndLoad
{
	public static class SaveAndLoadJson
	{
		public static void SaveCoinsToJson(int coinsAmount)
		{
			CoinsData data = new CoinsData();
			data.savedCoinsInJson = coinsAmount;
			string json = JsonUtility.ToJson(data,true);
			File.WriteAllText(Application.persistentDataPath + "/CoinsValue.json", json);
		}

		public static void LoadCoinsFromJson(int coinsAmount) 
		{
			if (!File.Exists(Application.persistentDataPath + "/CoinsValue.json"))
				return;

			string json = File.ReadAllText(Application.persistentDataPath + "/CoinsValue.json");
			CoinsData data = JsonUtility.FromJson<CoinsData>(json);
			coinsAmount = data.savedCoinsInJson;
		}
	} 
}