using TMPro;
using DG.Tweening;
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
        [SerializeField] TMP_Text coinsAddedText;

        void OnEnable()
        {
            BadDot.OnLoseGame += IncrementCoinsValue;
            UpgradePowerup.OnUpgradeBought += UpdateView;
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
                ShowAddedCoinsText();
            }
        }

        void ShowAddedCoinsText()
        {
            if (CoinsModel.CoinsToAdd > 0)
            {
                coinsAddedText.text = CoinsModel.CoinsToAdd.ToString();
                coinsAddedText.text = string.Format("{0}", coinsAddedText.text);
                coinsAddedText.gameObject.SetActive(true);
                coinsAddedText.rectTransform.DOAnchorPos(new Vector3(-35, -60, 0), 1f).OnComplete(() =>
                {
                    coinsAddedText.gameObject.SetActive(false);
                    coinsAddedText.rectTransform.DOAnchorPos(new Vector3(-35, 0, 0), 1f);
                }); 
            }
        }

        void OnDisable()
        {
            BadDot.OnLoseGame -= IncrementCoinsValue;
            UpgradePowerup.OnUpgradeBought -= UpdateView;
            IronSourceInit.OnCoinsRvWatched -= IncrementCoinsValueFromRV;
        }
    }
}