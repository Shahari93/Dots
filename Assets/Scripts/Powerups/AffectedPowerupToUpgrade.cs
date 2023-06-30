using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Coins.Model;
using Dots.Audio.Manager;
using Dots.Powerup.Model;
using Dots.GamePlay.Powerups;
using Dots.Utilities.SaveAndLoad;

namespace Dots.Powerup.Upgrade
{
    public class AffectedPowerupToUpgrade : MonoBehaviour, ISaveable
    {
        public static event Action<int> OnUpgradeBought;
        //public static event Action OnCoinsDecreaseAfterUpgrade;

        public static AffectedPowerupToUpgrade Instance;

        public PowerupEffectSO[] powerupEffectSOs;
        public Button[] upgradeButton;
        public TMP_Text[] powerupNameText;
        public TMP_Text[] powerupDurationText;
        public TMP_Text[] upgradeCoinsCostText;


        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            SaveAndLoadJson.LoadPowerupValues("/" + "Spawn Greens Powerup" + "PowerupValues.json", powerupEffectSOs[0]);
            SaveAndLoadJson.LoadPowerupValues("/" + "Slow Speed Powerup" + "PowerupValues.json", powerupEffectSOs[1]);
        }

        public void CallToUpgradePowerup(string powerupName)
        {
            int totalCoins = CoinsModel.CurrentCoinsAmount;
            for (int i = 0; i < powerupEffectSOs.Length; i++)
            {
                if (powerupEffectSOs[i].name == powerupName)
                {
                    if (powerupEffectSOs[i].powerupDuration >= powerupEffectSOs[i].powerupDurationLimit)
                    {
                        powerupEffectSOs[i].powerupDuration = powerupEffectSOs[i].powerupDurationLimit;
                        upgradeButton[i].interactable = false;
                        return;
                    }

                    totalCoins -= powerupEffectSOs[i].upgradeCoinsCost;
                    OnUpgradeBought?.Invoke(powerupEffectSOs[i].upgradeCoinsCost);
                    CoinsModel.CurrentCoinsAmount = totalCoins;

                    powerupEffectSOs[i].powerupDuration += 0.1f;
                    powerupEffectSOs[i].upgradeCoinsCost += 5;
                    PowerupUpgradesModel.CoinsCost = powerupEffectSOs[i].upgradeCoinsCost;
                    PowerupUpgradesModel.PowerupDurationValue = powerupEffectSOs[i].powerupDuration;
                    powerupDurationText[i].text = string.Format("{0} Seconds", powerupEffectSOs[i].powerupDuration.ToString("F1"));
                    upgradeCoinsCostText[i].text = string.Format("{0} Coins", powerupEffectSOs[i].upgradeCoinsCost);
                    SaveAndLoadJson.SavingToJson("/SavedData.json", this);
                    SaveAndLoadJson.SavePowerupValues("/" + powerupName + "PowerupValues.json", this, powerupEffectSOs[i]);

                    AudioManager.Instance.PlaySFX("Upgrade");
                }
            }
        }
    }
}