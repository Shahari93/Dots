using TMPro;
using UnityEngine;
using Dots.Coins.Model;
using Dots.GamePlay.Dot.Bad;
using DG.Tweening;

namespace Dots.Coins.Presenter
{
    public class CoinsPresenter : MonoBehaviour
    {
        [SerializeField] TMP_Text coinsText;
        [SerializeField] GameObject coinsPileParent;
        [SerializeField] Transform coinsFinalPos;

        private Vector3[] coinsPileInitialPosition = new Vector3[10];
        private Quaternion[] coinsPileInitialRotation = new Quaternion[10];

        private void OnEnable()
        {
            BadDot.OnLoseGame += IncrementCoinsValue;
        }

        private void Awake()
        {
            //UpdateView();
        }

        private void Start()
        {
            
            for (int i = 0; i < coinsPileParent.transform.childCount; i++)
            {
                coinsPileInitialPosition[i] = coinsPileParent.transform.GetChild(i).position;
                coinsPileInitialRotation[i] = coinsPileParent.transform.GetChild(i).rotation;
            }
        }

        private void ResetCoinsPile()
        {
            for (int i = 0; i < coinsPileParent.transform.childCount; i++)
            {
                coinsPileParent.transform.GetChild(i).position = coinsPileInitialPosition[i];
                coinsPileParent.transform.GetChild(i).rotation = coinsPileInitialRotation[i];
            }
        }

        void IncrementCoinsValue()
        {
            CoinsModel.Instance.UpdateCoinsData();
            UpdateView();
        }

        private void UpdateView()
        {
            if (CoinsModel.Instance == null)
                return;

            if (coinsPileParent == null && coinsFinalPos == null)
                return;

            ResetCoinsPile();

            float delay = 0f;
            coinsPileParent.SetActive(true);

            for (int i = 0; i < coinsPileParent.transform.childCount; i++)
            {
                coinsPileParent.transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
                coinsPileParent.GetComponent<RectTransform>().DOAnchorPos(coinsFinalPos.position, 1f).SetDelay(delay + 0.5f).SetEase(Ease.OutBack);
                coinsPileParent.transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1).SetEase(Ease.OutBack);
                delay += 0.2f;
            }

            if (coinsText != null)
            {
                coinsText.text = string.Format("{0}", CoinsModel.CurrentCoinsAmount.ToString());
            }
        }

        private void OnDisable()
        {
            BadDot.OnLoseGame -= IncrementCoinsValue;
        }
    }
}