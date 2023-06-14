using TMPro;
using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Audio.Manager;

namespace Dots.Utils.CoinsAnimation
{
    public class CoinsAnimation : MonoBehaviour
    {
        int coinsAmount;

        [SerializeField] GameObject coin;
        [SerializeField] GameObject pileOfCoins;
        [SerializeField] Transform target;
        [SerializeField] TMP_Text coinsAmountText;
        [SerializeField] Button restartGameButton;
        [SerializeField] Button returnHomeButton;

        [SerializeField] Vector2[] initialPos;
        [SerializeField] Quaternion[] initialRotation;

        public static event Action OnCoinsAnimationCompleted;

        void Start()
        {
            restartGameButton.interactable = false;
            returnHomeButton.interactable = false;
            coinsAmount = CoinsModel.CoinsToAdd;

            if (coinsAmount >= 10)
            {
                coinsAmount = 10;
            }

            for (int i = 0; i < coinsAmount; i++)
            {
                GameObject coinsChildren = Instantiate(coin, pileOfCoins.transform);
                coinsChildren.transform.localPosition = new Vector2(UnityEngine.Random.Range(-53.1f, 63f), UnityEngine.Random.Range(-65.4f, 40.2f));
            }

            initialPos = new Vector2[coinsAmount];
            initialRotation = new Quaternion[coinsAmount];

            for (int i = 0; i < pileOfCoins.transform.childCount; i++)
            {
                initialPos[i] = pileOfCoins.transform.GetChild(i).position;
                initialRotation[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation;
            }

            CountCoins();
        }

        void ResetInitValues()
        {
            for (int i = 0; i < pileOfCoins.transform.childCount; i++)
            {
                pileOfCoins.transform.GetChild(i).position = initialPos[i];
                pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation = initialRotation[i];
            }
        }

        void CountCoins()
        {
            if (CoinsModel.CoinsToAdd <= 0)
            {
                restartGameButton.interactable = true;
                returnHomeButton.interactable = true;
                return;
            }

            else
            {
                ResetInitValues();

                pileOfCoins.SetActive(true);

                var delay = 0f;

                for (int i = 0; i < pileOfCoins.transform.childCount; i++)
                {
                    pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay + 0.1f).SetEase(Ease.OutBack);

                    pileOfCoins.transform.GetChild(i).DOMove(target.position, 0.8f)
                        .SetDelay(delay + 0.5f).SetEase(Ease.InBack).OnComplete(() => AudioManager.Instance.PlaySFX("CoinsCollected"));


                    pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                        .SetEase(Ease.Flash);


                    pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

                    delay += 0.1f;

                    coinsAmountText.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f).OnComplete(() =>
                    {
                        OnCoinsAnimationCompleted?.Invoke();
                        restartGameButton.interactable = true;
                        returnHomeButton.interactable = true;
                    });
                }
            }
        }
    }
}