using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Utils.SaveAndLoad;
using System.Globalization;

namespace Dots.GamePlay.Powerups.Upgrade
{
    [Serializable]
    public class CoinsCostData
    {
        public int savedCoinsCostInJson;
    }
    public class UpgradePowerup : MonoBehaviour
    {
        private Button upgradeButton;

        public static event Action OnUpgradeBought;

        private static int coinsCost = 1; // TODO: Make this into a model 
        public static int CoinsCost
        {
            get
            {
                return coinsCost;
            }
            set
            {
                coinsCost = value;
            }
        }

        [SerializeField] PowerupEffectSO affectedPowerup;
        [SerializeField] TMP_Text powerupNameText;
        [SerializeField] TMP_Text powerupDurationText;
        [SerializeField] TMP_Text upgradeCoinsCostText;

        private void Awake()
        {
            upgradeButton = GetComponent<Button>();
            upgradeButton.onClick.AddListener(Upgrade);

            SaveAndLoadJson.LoadCoinsUpgradeFromJson();

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
                // Reducing the coins cost from the player total coins value and updating the model and the view
                totalCoins -= coinsCost;
                CoinsModel.Instance.UpdateCoinsDataAfterUpgrade(coinsCost);

                // Adding more coins for the coins cost to upgrade and updating the model and the view
                coinsCost += 1;
                CoinsModel.CurrentCoinsAmount = totalCoins;
                upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);

                // Updating the powerup duration and the view
                affectedPowerup.powerupDuration += 0.1f;
                powerupDurationText.text = string.Format("{0} Seconds", affectedPowerup.powerupDuration.ToString());

                // Checking if the player can still upgrade the powerups (If not the button turns inactive) and Sending an event to update the view
                CheckIfUpgradeable();
                OnUpgradeBought?.Invoke();
                SaveAndLoadJson.SaveCoinsUpgradeToJson();
            }
        }
    }
}