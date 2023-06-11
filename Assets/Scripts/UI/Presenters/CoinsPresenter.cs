using TMPro;
using UnityEngine;
using Dots.Ads.Init;
using Dots.Coins.Model;
using Dots.GamePlay.Dot.Bad;
using Dots.GamePlay.Powerups.Upgrade;

namespace Dots.Coins.Presenter
{
    public class CoinsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text coinsText;

        private void OnEnable()
        {
            BadDot.OnLoseGame += IncrementCoinsValue;
            UpgradePowerup.OnUpgradeBought += UpdateView;
            IronSourceInit.OnCoinsRvWatched += IncrementCoinsValueFromRV;
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

        void IncrementCoinsValueFromRV(int coinsToAdd)
        {
            CoinsModel.Instance.UpdateCoinsDataOnRv(coinsToAdd);
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
            IronSourceInit.OnCoinsRvWatched -= IncrementCoinsValueFromRV;
        }
    }
}