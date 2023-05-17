using TMPro;
using DG.Tweening;
using UnityEngine;
using Dots.Coins.Model;

public class CoinsAnimation : MonoBehaviour
{
    [SerializeField] private int coinsAmount;
    [SerializeField] TMP_Text coinsText;

    private Vector3[] initPosition;
    private Quaternion[] initRotation;


    void Start()
    {
        if (coinsAmount == 0)
            coinsAmount = 10; // you need to change this value based on the number of coins in the inspector

        initPosition = new Vector3[coinsAmount];
        initRotation = new Quaternion[coinsAmount];

        for (int i = 0; i < transform.childCount; i++)
        {
            initPosition[i] = transform.GetChild(i).GetComponent<RectTransform>().position;
            initRotation[i] = transform.GetChild(i).GetComponent<RectTransform>().rotation;
        }
    }

    private void ResetCoinsPile()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().position = initPosition[i];
            transform.GetChild(i).GetComponent<RectTransform>().rotation = initRotation[i];
        }
    }

    public void ShowCoinsAnimation()
    {
        if (CoinsModel.Instance == null)
            return;

        ResetCoinsPile();

        var delay = 0f;

        gameObject.SetActive(true);

        if (CoinsModel.CoinsToAdd > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                // Scale up
                transform.GetChild(i).DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);

                // Moving 
                transform.GetChild(i).GetComponent<RectTransform>().DOAnchorPos(new Vector2(445f, 800f), 1f)
                    .SetDelay(delay + 0.5f).SetEase(Ease.OutBack);


                transform.GetChild(i).DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f)
                    .SetEase(Ease.Flash);

                // Scale down

                transform.GetChild(i).DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);
                
                delay += 0.1f;
            }
        }
    }
}