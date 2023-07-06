   using UnityEngine;

namespace Dots.Feature.KeyAndChest.Prizes
{
    [CreateAssetMenu(fileName = "Prize")]
    public class ChestPrizeSO : ScriptableObject
	{
		public Sprite prizeImage;
		public int prizeAmount;
		public string prizeName;

		public int RandomizePrizeAmount()
		{
			return prizeAmount = Random.Range(5, 16);
		}
	} 
}