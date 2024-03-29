using TMPro;
using System;
using UnityEngine;
using Dots.Ads.Init;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Audio.Manager;
using Dots.Powerup.Model;
using Dots.GamePlay.Powerups;
using System.Threading.Tasks;
using Dots.Utilities.SaveAndLoad;
using Dots.Utilities.GooglePlayServices;

namespace Dots.Powerup.Upgrade
{
    public class AffectedPowerupToUpgrade : MonoBehaviour, ISaveable
    {
        public static event Action<int> OnUpgradeBought;

        public PowerupEffectSO[] powerupEffectSOs;
        public Button[] upgradeButtons;
        public TMP_Text[] powerupNameTexts;
        public TMP_Text[] powerupDurationTexts;
        public TMP_Text[] upgradeCoinsCostTexts;

        private int timesBought;

        void OnEnable()
        {
            IronSourceInit.OnCheckIfUpgradeable += CheckIfUpgradeable;

            SaveAndLoadJson.LoadPowerupValues("/" + powerupEffectSOs[0].name + " PowerupValues.json", powerupEffectSOs[0]);
            SaveAndLoadJson.LoadPowerupValues("/" + powerupEffectSOs[1].name + " PowerupValues.json", powerupEffectSOs[1]);
        }

        private void Awake()
        {
            if (PlayerPrefs.HasKey("TimesBought"))
            {
                timesBought = PlayerPrefs.GetInt("TimesBought");
            }
            else
            {
                timesBought = 0;
            }
        }

        void Start()
        {
            for (int i = 0; i < powerupEffectSOs.Length; i++)
            {
                powerupNameTexts[i].text = powerupEffectSOs[i].name;
                powerupDurationTexts[i].text = string.Format("{0} Seconds", powerupEffectSOs[i].powerupDuration.ToString("F1"));
                upgradeCoinsCostTexts[i].text = string.Format("{0} Coins", powerupEffectSOs[i].upgradeCoinsCost);
                upgradeButtons[i].interactable = CoinsModel.CurrentCoinsAmount >= powerupEffectSOs[i].upgradeCoinsCost 
                    || CoinsModel.CurrentCoinsAmount - powerupEffectSOs[i].upgradeCoinsCost > 0;
            }
        }

        bool CheckIfUpgradeable()
        {
            if (CoinsModel.CurrentCoinsAmount < PowerupUpgradesModel.CoinsCost || CoinsModel.CurrentCoinsAmount - PowerupUpgradesModel.CoinsCost < 0)
            {
                SetUpgradeButtonInteractable(2f);
                return false;
            }
            else
            {
                SetUpgradeButtonInteractable(1f);
                return true;
            }
        }

        private void SetUpgradeButtonInteractable(float divide)
        {
            for (int i = 0; i < upgradeButtons.Length; i++)
            {
                float alpha = 255f / divide;
                Color color = upgradeButtons[i].image.color;
                color.a = alpha;
                upgradeButtons[i].image.color = color;
            }
        }

        public void CallToUpgradePowerup(string powerupName)
        {
            int totalCoins = CoinsModel.CurrentCoinsAmount;

            for (int i = 0; i < powerupEffectSOs.Length; i++)
            {
                if (powerupEffectSOs[i].name == powerupName)
                {
                    AudioManager.Instance.PlaySFX("ButtonClick");

                    if (powerupEffectSOs[i].powerupDuration >= powerupEffectSOs[i].powerupDurationLimit)
                    {
                        powerupEffectSOs[i].powerupDuration = powerupEffectSOs[i].powerupDurationLimit;
                        upgradeButtons[i].interactable = false;
                        return;
                    }

                    totalCoins -= powerupEffectSOs[i].upgradeCoinsCost;
                    OnUpgradeBought?.Invoke(powerupEffectSOs[i].upgradeCoinsCost);
                    CoinsModel.CurrentCoinsAmount = totalCoins;

                    powerupEffectSOs[i].powerupDuration += 0.1f;
                    powerupEffectSOs[i].upgradeCoinsCost += 5;
                    PowerupUpgradesModel.CoinsCost = powerupEffectSOs[i].upgradeCoinsCost;
                    PowerupUpgradesModel.PowerupDurationValue = powerupEffectSOs[i].powerupDuration;
                    powerupDurationTexts[i].text = string.Format("{0} Seconds", powerupEffectSOs[i].powerupDuration.ToString("F1"));
                    upgradeCoinsCostTexts[i].text = string.Format("{0} Coins", powerupEffectSOs[i].upgradeCoinsCost);

                    upgradeButtons[i].interactable = CheckIfUpgradeable();

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
                    SaveAndLoadJson.SavePowerupValues("/" + powerupName + " PowerupValues.json", this, powerupEffectSOs[i]);

                    AudioManager.Instance.PlaySFX("Upgrade");
                }
                CheckIfUpgradeableAfterPurchase(i);
            }
        }

        async void CheckIfUpgradeableAfterPurchase(int upgrade)
        {
            await Task.Delay(100);
            upgradeButtons[upgrade].interactable = CoinsModel.CurrentCoinsAmount >= powerupEffectSOs[upgrade].upgradeCoinsCost
                    || CoinsModel.CurrentCoinsAmount - powerupEffectSOs[upgrade].upgradeCoinsCost > 0;
        }

        void OnDisable()
        {
            IronSourceInit.OnCheckIfUpgradeable -= CheckIfUpgradeable;
        }
    }
}