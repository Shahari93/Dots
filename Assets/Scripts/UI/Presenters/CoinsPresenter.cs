using TMPro;
using DG.Tweening;
using UnityEngine;
using Dots.Ads.Init;
using Dots.Coins.Model;
using Dots.GamePlay.Dot.Bad;
using Dots.Utilities.CoinsAnimation;
using Dots.GamePlay.Powerups.Upgrade;
using Dots.Powerup.Model;

namespace Dots.Coins.Presenter
{
    public class CoinsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text coinsText;
        [SerializeField] TMP_Text coinsAnimationText;

        void OnEnable()
        {
            IronSourceInit.OnCoinsRvWatched += IncrementCoinsValueFromRV;
            UpgradePowerup.OnUpgradeBought += ShowUsedCoinsText;
            UpgradePowerup.OnCoinsDecreaseAfterUpgrade += DecreaseCoinsAfterUpgrade;
            BadDot.OnLoseGame += IncrementCoinsValue;
            CoinsAnimation.OnCoinsAnimationCompleted += ShowAddedCoinsText;
        }

        void Awake()
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

        // Remove coins after upgrade purchase (Or any other future purchase)
        void DecreaseCoinsAfterUpgrade()
        {
            CoinsModel.Instance.UpdateCoinsData();
            UpdateView();
        }

        void UpdateView()
        {
            if (CoinsModel.Instance == null)
                return;

            if (coinsText != null)
            {
                coinsText.text = CoinsModel.CurrentCoinsAmount.ToString();
                coinsText.text = string.Format("{0}", coinsText.text);
            }
        }

        void ShowAddedCoinsText()
        {
            if (CoinsModel.CoinsToAdd > 0)
            {
                ShowCoinsText(coinsAnimationText, CoinsModel.CoinsToAdd, "+", new Vector3(-35, -60, 0), new Vector3(-35, 0, 0));
            }
        }

        void ShowUsedCoinsText()
        {
            for (int i = 0; i < PowerupUpgradesModel.CoinsCost.Length; i++)
            {
                ShowCoinsText(coinsAnimationText, PowerupUpgradesModel.CoinsCost[i], "-", new Vector3(-35, 0, 0), new Vector3(-35, -60, 0));
            }
        }

        void ShowCoinsText(TMP_Text coinsText, int coins, string sign, Vector3 startPos, Vector3 endPos)
        {
            coinsText.text = coins.ToString();
            coinsText.text = string.Format(sign + " {0}", coinsText.text);
            coinsText.gameObject.SetActive(true);
            coinsText.rectTransform.DOAnchorPos(endPos, 1f).OnComplete(() =>
            {
                coinsText.gameObject.SetActive(false);
                coinsText.rectTransform.DOAnchorPos(startPos, 1f);
                UpdateView();
            });
        }

        void OnDisable()
        {
            IronSourceInit.OnCoinsRvWatched -= IncrementCoinsValueFromRV;
            UpgradePowerup.OnUpgradeBought -= ShowUsedCoinsText;
            UpgradePowerup.OnCoinsDecreaseAfterUpgrade -= DecreaseCoinsAfterUpgrade;
            BadDot.OnLoseGame -= IncrementCoinsValue;
            CoinsAnimation.OnCoinsAnimationCompleted -= ShowAddedCoinsText;
        }
    }
}