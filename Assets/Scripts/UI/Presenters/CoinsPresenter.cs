using TMPro;
using DG.Tweening;
using UnityEngine;
using Dots.Ads.Init;
using Dots.Coins.Model;
using Dots.GamePlay.Dot.Bad;
using Dots.Utils.CoinsAnimation;
using Dots.GamePlay.Powerups.Upgrade;

namespace Dots.Coins.Presenter
{
    public class CoinsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text coinsText;
        [SerializeField] TMP_Text coinsAnimationText;

        void OnEnable()
        {
            BadDot.OnLoseGame += IncrementCoinsValue;
            UpgradePowerup.OnUpgradeBought += ShowUsedCoinsText;

            CoinsAnimation.OnCoinsAnimationCompleted += ShowAddedCoinsText;
            IronSourceInit.OnCoinsRvWatched += IncrementCoinsValueFromRV;
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

        //TODO: Find a way to make this 2 functions (ShowAddedCoinsText() and ShowUsedCoinsText()) to be more generic
        void ShowAddedCoinsText()
        {
            if (CoinsModel.CoinsToAdd > 0)
            {
                coinsAnimationText.text = CoinsModel.CoinsToAdd.ToString();
                coinsAnimationText.text = string.Format("+ {0}", coinsAnimationText.text);
                coinsAnimationText.gameObject.SetActive(true);
                coinsAnimationText.rectTransform.DOAnchorPos(new Vector3(-35, 0, 0), 1f).OnComplete(() =>
                {
                    coinsAnimationText.gameObject.SetActive(false);
                    coinsAnimationText.rectTransform.DOAnchorPos(new Vector3(-35, -60, 0), 1f);
                });
            }
        }

        void ShowUsedCoinsText()
        {
            coinsAnimationText.text = UpgradePowerup.CoinsCost.ToString();
            coinsAnimationText.text = string.Format("- {0}", coinsAnimationText.text);
            coinsAnimationText.gameObject.SetActive(true);
            coinsAnimationText.rectTransform.DOAnchorPos(new Vector3(-35, -60, 0), 1f).OnComplete(() =>
            {
                coinsAnimationText.gameObject.SetActive(false);
                coinsAnimationText.rectTransform.DOAnchorPos(new Vector3(-35, 0, 0), 1f);
                UpdateView();
            });
        }

        void OnDisable()
        {
            BadDot.OnLoseGame -= IncrementCoinsValue;
            IronSourceInit.OnCoinsRvWatched -= IncrementCoinsValueFromRV;

            CoinsAnimation.OnCoinsAnimationCompleted -= ShowAddedCoinsText;
            UpgradePowerup.OnUpgradeBought -= ShowUsedCoinsText;
        }
    }
}