using TMPro;
using System;
using UnityEngine;
using Dots.Ads.Init;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Powerup.Model;
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

        [SerializeField] PowerupEffectSO[] affectedPowerup;
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
            PowerupUpgradesModel.CoinsCost = new int[affectedPowerup.Length];
            PowerupUpgradesModel.PowerupDurationValue = new float[affectedPowerup.Length];

            if (PlayerPrefs.HasKey("TimesBought"))
            {
                timesBought = PlayerPrefs.GetInt("TimesBought");
            }
            else
            {
                timesBought = 0;
            }
            upgradeButton.onClick.AddListener(Upgrade);
            SaveAndLoadJson.LoadFromJson("/SavedData.json");

            SaveAndLoadJson.LoadPowerupValues("/PowerupValues.json", affectedPowerup[0]);
            for (int i = 0; i < affectedPowerup.Length; i++)
            {

                PowerupUpgradesModel.CoinsCost[i] = affectedPowerup[i].upgradeCoinsCost;
                PowerupUpgradesModel.PowerupDurationValue[i] = affectedPowerup[i].powerupDuration;
                powerupNameText.text = affectedPowerup[i].name;
                powerupDurationText.text = string.Format("{0} Seconds", PowerupUpgradesModel.PowerupDurationValue[i].ToString("F1"));
                upgradeCoinsCostText.text = string.Format("{0} Coins", PowerupUpgradesModel.CoinsCost[i]);
            }
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
            for (int i = 0; i < affectedPowerup.Length; i++)
            {
                if (affectedPowerup[i].powerupDuration >= affectedPowerup[i].powerupDurationLimit)
                {
                    affectedPowerup[i].powerupDuration = affectedPowerup[i].powerupDurationLimit;

                    return upgradeButton.interactable = false;
                }

                if (CoinsModel.CurrentCoinsAmount < PowerupUpgradesModel.CoinsCost[i])
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
            return upgradeButton.interactable = false;
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

                for (int i = 0; i < affectedPowerup.Length; i++)
                {
                    totalCoins -= affectedPowerup[i].upgradeCoinsCost;
                    OnCoinsDecreaseAfterUpgrade?.Invoke();

                    affectedPowerup[i].upgradeCoinsCost += 5;
                    CoinsModel.CurrentCoinsAmount = totalCoins;
                    upgradeCoinsCostText.text = string.Format("{0} Coins", affectedPowerup[i].upgradeCoinsCost);

                    affectedPowerup[i].powerupDuration += 0.1f;
                    powerupDurationText.text = string.Format("{0} Seconds", affectedPowerup[i].powerupDuration.ToString("F1"));

                    CheckIfUpgradeable();
                    AudioManager.Instance.PlaySFX("Upgrade");

                    timesBought++;
                    if (timesBought == 1)
                    {
                        if (GoogleServices.Instance.connectedToGooglePlay)
                        {
                            Social.ReportProgress("CgkIm-Xn1MEZEAIQDQ", 100.0f, null);
                        }
                    }
                    PlayerPrefs.SetInt("TimesBought", timesBought);

                    PowerupUpgradesModel.CoinsCost[i] = affectedPowerup[i].upgradeCoinsCost;
                    PowerupUpgradesModel.PowerupDurationValue[i] = affectedPowerup[i].powerupDuration;
                }
                    SaveAndLoadJson.SavePowerupValues("/PowerupValues.json", this, affectedPowerup[0]);
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);
            }
        }

        void OnDisable()
        {
            IronSourceInit.OnCheckIfUpgradeable -= CheckIfUpgradeable;
            for (int i = 0; i < affectedPowerup.Length; i++)
            {
                SaveAndLoadJson.SavePowerupValues("/PowerupValues.json", this, affectedPowerup[i]);
            }
        }

        void OnDestroy()
        {
            upgradeButton.onClick.RemoveListener(Upgrade);
            for (int i = 0; i < affectedPowerup.Length; i++)
            {
                SaveAndLoadJson.SavePowerupValues("/PowerupValues.json", this, affectedPowerup[i]);
            }
        }
    }
}