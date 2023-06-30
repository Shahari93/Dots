using UnityEngine;
using Dots.Ads.Init;
using Dots.Coins.Model;
using Dots.Powerup.Model;
using Dots.Audio.Manager;
using Dots.Powerup.Upgrade;
using Dots.Utilities.SaveAndLoad;
using Dots.Utilities.GooglePlayServices;

namespace Dots.GamePlay.Powerups.Upgrade
{
    /// <summary>
    /// This class is responsible for upgrading powerups
    /// Here we can buy duration upgrade for powerups that have duration (For now it's the spawn green dots powerup)
    /// </summary>
    public class UpgradePowerup : MonoBehaviour, ISaveable
    {
        private int timesBought;
        
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

            SaveAndLoadJson.LoadFromJson("/SavedData.json");
        }

        void Start()
        {
            for (int i = 0; i < AffectedPowerupToUpgrade.Instance.powerupEffectSOs.Length; i++)
            {
                AffectedPowerupToUpgrade.Instance.powerupNameText[i].text = AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].name;
                AffectedPowerupToUpgrade.Instance.powerupDurationText[i].text = string.Format("{0} Seconds", AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].powerupDuration.ToString("F1"));
                AffectedPowerupToUpgrade.Instance.upgradeCoinsCostText[i].text = string.Format("{0} Coins", AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].upgradeCoinsCost);
                AffectedPowerupToUpgrade.Instance.upgradeButton[i].interactable = CheckIfUpgradeable();
            }
        }

        /// <summary>
        /// Checking if the button needs to be interactable or not based on the amount of coins the player have
        /// </summary>
        /// <returns> returns true or false based on the amount of coins the player have</returns>
        bool CheckIfUpgradeable()
        {
            for (int i = 0; i < AffectedPowerupToUpgrade.Instance.powerupEffectSOs.Length; i++)
            {
                if (CoinsModel.CurrentCoinsAmount < PowerupUpgradesModel.CoinsCost)
                {
                    SetUpgradeButtonInteractable(2f);
                    return AffectedPowerupToUpgrade.Instance.upgradeButton[i].interactable = false;
                }
                else
                {
                    SetUpgradeButtonInteractable(1f);
                    return AffectedPowerupToUpgrade.Instance.upgradeButton[i].interactable = true;
                }
            }
            return false;
        }
        /// <summary>
        /// Sets the button interactable state and color
        /// </summary>
        /// <param name="divide"></param>
        private void SetUpgradeButtonInteractable(float divide)
        {
            for (int i = 0; i < AffectedPowerupToUpgrade.Instance.upgradeButton.Length; i++)
            {
                float alpha = 255f / divide;
                Color color = AffectedPowerupToUpgrade.Instance.upgradeButton[i].image.color;
                color.a = alpha;
                AffectedPowerupToUpgrade.Instance.upgradeButton[i].image.color = color;
            }
        }

        /// <summary>
        /// The logic of upgrade button
        /// We invoke events when we buy the upgrades and we save it to a json file
        /// </summary>
        public void Upgrade(PowerupEffectSO powerup)
        {
            
            if (CheckIfUpgradeable())
            {
                AudioManager.Instance.PlaySFX("ButtonClick");
                AffectedPowerupToUpgrade.Instance.CallToUpgradePowerup(powerup.name);
                CheckIfUpgradeable();
                
                timesBought++;
                if (timesBought == 1)
                {
                    if (GoogleServices.Instance.connectedToGooglePlay)
                    {
                        Social.ReportProgress("CgkIm-Xn1MEZEAIQDQ", 100.0f, null);
                    }
                }
                PlayerPrefs.SetInt("TimesBought", timesBought);
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);

                //for (int i = 0; i < AffectedPowerupToUpgrade.Instance.powerupEffectSOs.Length; i++)
                //{
                //    
                //    OnCoinsDecreaseAfterUpgrade?.Invoke();

                //    AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].upgradeCoinsCost += 5;
                //    CoinsModel.CurrentCoinsAmount = totalCoins;
                //    upgradeCoinsCostText.text = string.Format("{0} Coins", AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].upgradeCoinsCost);

                //    AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].powerupDuration += 0.1f;
                //    powerupDurationText.text = string.Format("{0} Seconds", AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].powerupDuration.ToString("F1"));

                //    CheckIfUpgradeable();
                //    AudioManager.Instance.PlaySFX("Upgrade");

                //    timesBought++;
                //    if (timesBought == 1)
                //    {
                //        if (GoogleServices.Instance.connectedToGooglePlay)
                //        {
                //            Social.ReportProgress("CgkIm-Xn1MEZEAIQDQ", 100.0f, null);
                //        }
                //    }
                //    PlayerPrefs.SetInt("TimesBought", timesBought);

                //    PowerupUpgradesModel.CoinsCost[i] = AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].upgradeCoinsCost;
                //    PowerupUpgradesModel.PowerupDurationValue[i] = AffectedPowerupToUpgrade.Instance.powerupEffectSOs[i].powerupDuration;
                //}
            }
        }

        void OnDisable()
        {
            IronSourceInit.OnCheckIfUpgradeable -= CheckIfUpgradeable;
        }
    }
}