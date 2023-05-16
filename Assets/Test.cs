using Dots.Coins.Model;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject coinsPanel;
    [SerializeField] TMP_Text coinsText;

    // Update is called once per frame
    void Update()
    {
        coinsText.text = CoinsModel.CurrentCoinsAmount.ToString();
    }
}
