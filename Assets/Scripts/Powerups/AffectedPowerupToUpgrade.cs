using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Powerup.Model;
using Dots.GamePlay.Powerups;
using Dots.Utilities.SaveAndLoad;
using Dots.Audio.Manager;
using Dots.Coins.Model;

public class AffectedPowerupToUpgrade : MonoBehaviour, ISaveable
{
    public static event Action OnUpgradeBought;
    public static event Action OnCoinsDecreaseAfterUpgrade;

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

        PowerupUpgradesModel.CoinsCost = new int[powerupEffectSOs.Length];
        PowerupUpgradesModel.PowerupDurationValue = new float[powerupEffectSOs.Length];
        //for (int i = 0; i < powerupEffectSOs.Length; i++)
        //{
        //    SaveAndLoadJson.LoadPowerupValues("/" + powerupEffectSOs[i].name + "PowerupValues.json", powerupEffectSOs[i]);
        //}
    }

    private void Start()
    {
        for (int i = 0; i < powerupEffectSOs.Length; i++)
        {
            PowerupUpgradesModel.CoinsCost[i] = powerupEffectSOs[i].upgradeCoinsCost;
            PowerupUpgradesModel.PowerupDurationValue[i] = powerupEffectSOs[i].powerupDuration;
        }
    }

    public void CallToUpgradePowerup(string powerupName)
    {
        int totalCoins = CoinsModel.CurrentCoinsAmount;
        for (int i = 0; i < powerupEffectSOs.Length; i++)
        {
            if (powerupEffectSOs[i].name == powerupName)
            {
                totalCoins -= powerupEffectSOs[i].upgradeCoinsCost;
                OnUpgradeBought?.Invoke();
                PowerupUpgradesModel.CoinsCost[i] = powerupEffectSOs[i].upgradeCoinsCost;
                PowerupUpgradesModel.PowerupDurationValue[i] = powerupEffectSOs[i].powerupDuration;
                CoinsModel.CurrentCoinsAmount = totalCoins;


                powerupEffectSOs[i].powerupDuration += 0.1f;
                powerupEffectSOs[i].upgradeCoinsCost += 5;
                powerupDurationText[i].text = string.Format("{0} Seconds", powerupEffectSOs[i].powerupDuration.ToString("F1"));
                upgradeCoinsCostText[i].text = string.Format("{0} Coins", powerupEffectSOs[i].upgradeCoinsCost);
                SaveAndLoadJson.SavingToJson("/SavedData.json", this);
                SaveAndLoadJson.SavePowerupValues("/" + powerupName + "PowerupValues.json", this, powerupEffectSOs[i]);
                AudioManager.Instance.PlaySFX("Upgrade");
                OnCoinsDecreaseAfterUpgrade?.Invoke();
            }
        }
    }
}