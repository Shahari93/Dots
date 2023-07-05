using TMPro;
using UnityEngine;

namespace Dots.Feature.KeyAndChest.Prizes.Holder
{
    public class ChestPrizeHolder : MonoBehaviour
    {
        public ChestPrizeSO prizeSO;
        //[SerializeField] TMP_Text prizeText;

        private void Awake()
        {
            prizeSO.RandomizePrizeAmount();
            //prizeText.text = string.Format("{0} Coins", prizeSO.prizeAmount);
        }
    }
}