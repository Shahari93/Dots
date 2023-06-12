using TMPro;
using System;
using UnityEngine;
using Dots.Ads.Init;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Audio.Manager;
using Dots.Utils.SaveAndLoad;

namespace Dots.GamePlay.Powerups.Upgrade
{
    [Serializable]
    public class CoinsCostData
    {
        public int savedCoinsCostInJson;
    }
    public class UpgradePowerup : MonoBehaviour, ISaveable
    {
        public static event Action OnUpgradeBought;

        static float powerupDurationValue;
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

        static int coinsCost = 10; // TODO: Make this into a model 
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

        void OnEnable()
        {
            IronSourceInit.OnCheckIfUpgradeable += CheckIfUpgradeable;
        }

        void Awake()
        {
            powerupDurationValue = affectedPowerup.powerupDuration;
            upgradeButton.onClick.AddListener(Upgrade);

            SaveAndLoadJson.LoadFromJson("/SavedData.json");
            affectedPowerup.powerupDuration = powerupDurationValue;

            powerupNameText.text = affectedPowerup.name;
            powerupDurationValue = affectedPowerup.powerupDuration;
            powerupDurationText.text = string.Format("{0} Seconds", powerupDurationValue.ToString("F1"));
            upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);
        }

        void Start()
        {
            upgradeButton.interactable = CheckIfUpgradeable();
        }

        // TODO: Make this more generic - The only difference is if the button is inactive we divide the alpha by 2
        bool CheckIfUpgradeable()
        {
            if (affectedPowerup.powerupDuration >= affectedPowerup.powerupDurationLimit)
            {
                affectedPowerup.powerupDuration = affectedPowerup.powerupDurationLimit;
                
                return upgradeButton.interactable = false;
            }

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

        void Upgrade()
        {
            int totalCoins = CoinsModel.CurrentCoinsAmount;
            if (CheckIfUpgradeable())
            {
                AudioManager.Instance.PlaySFX("ButtonClick");
                // Reducing the coins cost from the player total coins value and updating the model and the view
                totalCoins -= coinsCost;
                CoinsModel.Instance.UpdateCoinsDataAfterUpgrade(coinsCost); // TODO: Make sure this is the right place to use this method (Probably should be in the CoinsPresenter)

                // Adding more coins for the coins cost to upgrade and updating the model and the view
                coinsCost += 5;
                CoinsModel.CurrentCoinsAmount = totalCoins;
                upgradeCoinsCostText.text = string.Format("{0} Coins", coinsCost);

                // Updating the powerup duration and the view
                powerupDurationValue += 0.1f;
                affectedPowerup.powerupDuration = powerupDurationValue;
                powerupDurationText.text = string.Format("{0} Seconds", powerupDurationValue.ToString("F1"));

                // Checking if the player can still upgrade the powerups (If not the button turns inactive) and Sending an event to update the view
                CheckIfUpgradeable();
                AudioManager.Instance.PlaySFX("Upgrade");
                OnUpgradeBought?.Invoke();
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);
            }
        }

        void OnDisable()
        {
            IronSourceInit.OnCheckIfUpgradeable -= CheckIfUpgradeable;
        }
    }
}