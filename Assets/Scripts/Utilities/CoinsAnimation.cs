using TMPro;
using UnityEngine;
using DG.Tweening;
using Dots.Coins.Model;
using System.Collections;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace Dots.Utils.CoinsAnimation
{
    public class CoinsAnimation : MonoBehaviour
    {
        private int coinsAmount;

        [SerializeField] private GameObject coin;
        [SerializeField] private GameObject pileOfCoins;
        [SerializeField] private Transform target;
        [SerializeField] private TMP_Text coinsAmountText;
        [SerializeField] private Button restartGameButton;

        [SerializeField] private Vector2[] initialPos;
        [SerializeField] private Quaternion[] initialRotation;

        void Start()
        {
            restartGameButton.interactable = false;
            coinsAmount = CoinsModel.CoinsToAdd;

            if(coinsAmount >= 10)
            {
                coinsAmount = 10;
            }

            for (int i = 0; i < coinsAmount; i++)
            {
                GameObject coinsChildren = Instantiate(coin, pileOfCoins.transform);

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

        private void ResetInitValues()
        {
            for (int i = 0; i < pileOfCoins.transform.childCount; i++)
            {
                pileOfCoins.transform.GetChild(i).position = initialPos[i];
                pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation = initialRotation[i];
            }
        }

        private async void CountCoins()
        {
            await Task.Delay(1000);
            restartGameButton.interactable = true;

            if (CoinsModel.CoinsToAdd <= 0)
                return;

            else
            {
                ResetInitValues();

                pileOfCoins.SetActive(true);

                var delay = 0f;

                for (int i = 0; i < pileOfCoins.transform.childCount; i++)
                {
                    pileOfCoins.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay + 0.1f).SetEase(Ease.OutBack);

                    pileOfCoins.transform.GetChild(i).DOMove(target.position, 0.8f)
                        .SetDelay(delay + 0.5f).SetEase(Ease.InBack);


                    pileOfCoins.transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                        .SetEase(Ease.Flash);


                    pileOfCoins.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);

                    delay += 0.1f;

                    coinsAmountText.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
                }
            }
        }
    }
}