using DG.Tweening;
using Dots.Coins.Model;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Testing : MonoBehaviour
{
    [SerializeField] private GameObject pileOfCoins;
    [SerializeField] private Transform target;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Vector2[] initialPos;
    [SerializeField] private Quaternion[] initialRotation;
    [SerializeField] private int coinsAmount;
    [SerializeField] private Button button;

    void Start()
    {
        if (coinsAmount == 0)
            coinsAmount = CoinsModel.CoinsToAdd;

        initialPos = new Vector2[coinsAmount];
        initialRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            initialPos[i] = pileOfCoins.transform.GetChild(i).position;
            initialRotation[i] = pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
    }

    private void ResetInitValues()
    {
        for (int i = 0; i < pileOfCoins.transform.childCount; i++)
        {
            pileOfCoins.transform.GetChild(i).position = initialPos[i];
            pileOfCoins.transform.GetChild(i).GetComponent<RectTransform>().rotation = initialRotation[i];
        }
    }

    public void CountCoins()
    {
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

                counter.transform.parent.GetChild(0).transform.DOScale(1.1f, 0.1f).SetLoops(10, LoopType.Yoyo).SetEase(Ease.InOutSine).SetDelay(1.2f);
            }

            StartCoroutine(CountDollars());
        }
    }

    // TODO: Fix to show real coins amount
    IEnumerator CountDollars()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerPrefs.SetInt("CountDollar", PlayerPrefs.GetInt("CountDollar") + 50 + PlayerPrefs.GetInt("BPrize"));
        counter.text = PlayerPrefs.GetInt("CountDollar").ToString();
        PlayerPrefs.SetInt("BPrize", 0);
    }
}
