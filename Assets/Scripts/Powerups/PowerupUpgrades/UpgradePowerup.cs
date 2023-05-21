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

        [SerializeField] private int coinsCost;

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
                return false;
            }
            else
                return true;
        }

        private void Upgrade()
        {
            throw new NotImplementedException();
        }
    }
}