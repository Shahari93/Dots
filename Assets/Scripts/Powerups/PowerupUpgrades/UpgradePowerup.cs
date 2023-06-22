using TMPro;
using System;
using UnityEngine;
using Dots.Ads.Init;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Audio.Manager;
using Dots.Utilities.SaveAndLoad;
using Dots.Utilities.GooglePlayServices;

namespace Dots.GamePlay.Powerups.Upgrade
{
    [Serializable]
    public class CoinsCostData
    {
        public int savedCoinsCostInJson;
    }
    /// <summary>
    /// This class is responsible for upgrading powerups
    /// Here we can buy duration upgrade for powerups that have duration (For now it's the spawn green dots powerup)
    /// </summary>
    public class UpgradePowerup : MonoBehaviour, ISaveable
    {
        private int timesBought;
        public static event Action OnUpgradeBought;
        public static event Action OnCoinsDecreaseAfterUpgrade;

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

        /// <summary>
        /// Setting the initial value for the text
        /// </summary>
        void Awake()
        {
            if (PlayerPrefs.HasKey("TimesBought"))
            {
                timesBought = PlayerPrefs.GetInt("TimesBought");
            }
            else
            {
                timesBought = 0;
            }
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

        /// <summary>
        /// Checking if the button needs to be interactable or not based on the amount of coins the player have
        /// </summary>
        /// <returns> returns true or false based on the amount of coins the player have</returns>
        bool CheckIfUpgradeable()
        {
            if (affectedPowerup.powerupDuration >= affectedPowerup.powerupDurationLimit)
            {
                affectedPowerup.powerupDuration = affectedPowerup.powerupDurationLimit;
                
                return upgradeButton.interactable = false;
            }

            if (CoinsModel.CurrentCoinsAmount < coinsCost)
            {
                SetUpgradeButtonInteractable(2f);
                return upgradeButton.interactable = false;
            }
            else
            {
                SetUpgradeButtonInteractable(1f);
                return upgradeButton.interactable = true;
            }
        }
        /// <summary>
        /// Sets the button interactable state and color
        /// </summary>
        /// <param name="divide"></param>
        private void SetUpgradeButtonInteractable(float divide)
        {
            float alpha = 255f / divide;
            Color color = upgradeButton.image.color;
            color.a = alpha;
            upgradeButton.image.color = color;
        }

        /// <summary>
        /// The logic of upgrade button
        /// We invoke events when we buy the upgrades and we save it to a json file
        /// </summary>
        void Upgrade()
        {
            int totalCoins = CoinsModel.CurrentCoinsAmount;
            if (CheckIfUpgradeable())
            {
                OnUpgradeBought?.Invoke();
                AudioManager.Instance.PlaySFX("ButtonClick");
                // Reducing the coins cost from the player total coins value and updating the model and the view
                totalCoins -= coinsCost;
                OnCoinsDecreaseAfterUpgrade?.Invoke();

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
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);
                timesBought++;
                if(timesBought == 1)
                {
                    if (GoogleServices.Instance.connectedToGooglePlay)
                    {
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQDQ", 100.0f, null);
                    }
                }
                PlayerPrefs.SetInt("TimesBought", timesBought);
            }
        }

        void OnDisable()
        {
            IronSourceInit.OnCheckIfUpgradeable -= CheckIfUpgradeable;
        }

        void OnDestroy()
        {
            upgradeButton.onClick.RemoveListener(Upgrade);
        }
    }
}