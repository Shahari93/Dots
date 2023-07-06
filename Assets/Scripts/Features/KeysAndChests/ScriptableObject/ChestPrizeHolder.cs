using TMPro;
using UnityEngine;

namespace Dots.Feature.KeyAndChest.Prizes.Holder
{
    public class ChestPrizeHolder : MonoBehaviour
    {
        public ChestPrizeSO prizeSO;
        public TMP_Text prizeText;

        private void Awake()
        {
            prizeSO.RandomizePrizeAmount();
            prizeText.text = string.Format("{0} {1}", prizeSO.prizeAmount, prizeSO.prizeName);
            prizeText.gameObject.SetActive(false);
        }
    }
}