using Dots.Coins.Model;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dots.GamePlay.Powerups.Upgrade
{
    public class UpgradePowerup : MonoBehaviour
    {
        private Button upgradeButton;

        public static event Action OnUpgradeBought;

        [SerializeField] private int coinsCost; // TODO: Make this into a model 

        [SerializeField] PowerupEffectSO affectedPowerup;
        [SerializeField] TMP_Text powerupNameText;
        [SerializeField] TMP_Text powerupDurationText;
        [SerializeField] TMP_Text upgradeCoinsCostText;

        private void Awake()
        {
            upgradeButton = GetComponent<Button>();
            upgradeButton.onClick.AddListener(Upgrade);

            powerupNameText.text = affectedPowerup.name;
            powerupDurationText.text = string.Format("{0} Seconds", affectedPowerup.powerupDuration.ToString());
            upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);
        }

        private void Start()
        {
            upgradeButton.interactable = CheckIfUpgradeable();
        }

        private bool CheckIfUpgradeable()
        {
            if (CoinsModel.CurrentCoinsAmount < coinsCost)
            {
                return upgradeButton.interactable = false;
            }
            else
                return upgradeButton.interactable = true;
        }

        private void Upgrade()
        {
            int totalCoins = CoinsModel.CurrentCoinsAmount; 
            if (CheckIfUpgradeable())
            {
                totalCoins -= coinsCost;
                CoinsModel.Instance.UpdateCoinsDataAfterUpgrade(coinsCost);
                coinsCost += 10;
                CoinsModel.CurrentCoinsAmount = totalCoins;
                upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);
                CheckIfUpgradeable();
                affectedPowerup.powerupDuration += 0.1f;
                powerupDurationText.text = string.Format("{0} Seconds", affectedPowerup.powerupDuration.ToString());
                OnUpgradeBought?.Invoke(); 
            }
        }
    }
}