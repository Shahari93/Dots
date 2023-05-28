using TMPro;
using System;
using UnityEngine;
using Dots.Ads.Init;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Utils.SaveAndLoad;

namespace Dots.GamePlay.Powerups.Upgrade
{
    [Serializable]
    public class CoinsCostData
    {
        public int savedCoinsCostInJson;
    }
    public class UpgradePowerup : MonoBehaviour
    {
        public static event Action OnUpgradeBought;

        private static float powerupDurationValue;
        public static float PowerupDurationValue
        {
            get
            {
                return powerupDurationValue;
            }
            set
            {
                powerupDurationValue = value;
            }
        }

        private static int coinsCost = 10; // TODO: Make this into a model 
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
        [SerializeField] Button upgradeButton;
        [SerializeField] TMP_Text powerupNameText;
        [SerializeField] TMP_Text powerupDurationText;
        [SerializeField] TMP_Text upgradeCoinsCostText;

        private void OnEnable()
        {
            IronSourceInit.OnCheckIfUpgradeable += CheckIfUpgradeable;
        }

        private void Awake()
        {
            powerupDurationValue = affectedPowerup.powerupDuration;
            upgradeButton.onClick.AddListener(Upgrade);

            SaveAndLoadJson.LoadCoinsUpgradeFromJson();
            SaveAndLoadJson.LoadPowerupDurationFromJson();

            affectedPowerup.powerupDuration = powerupDurationValue;

            powerupNameText.text = affectedPowerup.name;
            powerupDurationValue = affectedPowerup.powerupDuration;
            powerupDurationText.text = string.Format("{0} Seconds", powerupDurationValue.ToString("F1"));
            upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);
        }

        private void Start()
        {
            upgradeButton.interactable = CheckIfUpgradeable();
        }

        // TODO: Make this more generic - The only difference is if the button is inactive we divide the alpha by 2
        private bool CheckIfUpgradeable()
        {
            if (CoinsModel.CurrentCoinsAmount < coinsCost)
            {
                float alpha = 255f / 2;
                Color color = upgradeButton.image.color;
                color.a = alpha;
                upgradeButton.image.color = color;
                return upgradeButton.interactable = false;
            }
            else
            {
                float alpha = 255;
                Color color = upgradeButton.image.color;
                color.a = alpha;
                upgradeButton.image.color = color;
                return upgradeButton.interactable = true;
            }
        }

        private void Upgrade()
        {
            int totalCoins = CoinsModel.CurrentCoinsAmount;
            if (CheckIfUpgradeable())
            {
                // Reducing the coins cost from the player total coins value and updating the model and the view
                totalCoins -= coinsCost;
                CoinsModel.Instance.UpdateCoinsDataAfterUpgrade(coinsCost); // TODO: Make sure this is the right place to use this method (Probably should be in the CoinsPresenter)

                // Adding more coins for the coins cost to upgrade and updating the model and the view
                coinsCost += 10;
                CoinsModel.CurrentCoinsAmount = totalCoins;
                upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);

                // Updating the powerup duration and the view
                powerupDurationValue += 0.1f;
                affectedPowerup.powerupDuration = powerupDurationValue;
                powerupDurationText.text = string.Format("{0} Seconds", powerupDurationValue.ToString("F1"));

                // Checking if the player can still upgrade the powerups (If not the button turns inactive) and Sending an event to update the view
                CheckIfUpgradeable();
                OnUpgradeBought?.Invoke();
                SaveAndLoadJson.SaveCoinsUpgradeToJson();
                SaveAndLoadJson.SavePowerupDurationToJson();
            }
        }

        private void OnDisable()
        {
            IronSourceInit.OnCheckIfUpgradeable -= CheckIfUpgradeable;
        }
    }
}