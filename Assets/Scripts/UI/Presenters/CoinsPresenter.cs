using TMPro;
using UnityEngine;
using Dots.Coins.Model;
using Dots.GamePlay.Dot.Bad;
using Dots.GamePlay.Powerups.Upgrade;
using Dots.Ads.Init;

namespace Dots.Coins.Presenter
{
    public class CoinsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text coinsText;

        private void OnEnable()
        {
            BadDot.OnLoseGame += IncrementCoinsValue;
            UpgradePowerup.OnUpgradeBought += UpdateView;
            IronSourceInit.OnCoinsRvWatched += IncrementCoinsValue;
        }

        private void Awake()
        {
            UpdateView();
        }

        void IncrementCoinsValue()
        {
            CoinsModel.Instance.UpdateCoinsData();
            UpdateView();
        }

        void IncrementCoinsValue(int coins)
        {
            CoinsModel.Instance.UpdateCoinsDataOnRv(coins);
            UpdateView();
        }

        private void UpdateView()
        {
            if (CoinsModel.Instance == null)
                return;

            if (coinsText != null)
            {
                coinsText.text = string.Format("{0}", CoinsModel.CurrentCoinsAmount.ToString());
            }
        }

        private void OnDisable()
        {
            BadDot.OnLoseGame -= IncrementCoinsValue;
            UpgradePowerup.OnUpgradeBought -= UpdateView;
            IronSourceInit.OnCoinsRvWatched -= IncrementCoinsValue;
        }
    }
}