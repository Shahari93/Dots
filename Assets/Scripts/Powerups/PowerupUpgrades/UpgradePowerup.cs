using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dots.GamePlay.Powerups.Upgrade
{
	public class UpgradePowerup : MonoBehaviour
	{
        private Button upgradeButton;

		[SerializeField] PowerupEffectSO affectedPowerup;
		[SerializeField] TMP_Text powerupNameText;
		[SerializeField] TMP_Text powerupDurationText;
		[SerializeField] TMP_Text upgradeCoinsCostText;

        private void Awake()
        {
            upgradeButton = GetComponent<Button>();
            upgradeButton.onClick.AddListener(Upgrade);

            powerupNameText.text = affectedPowerup.name;
            powerupDurationText.text = affectedPowerup.powerupDuration.ToString();
        }

        private void Upgrade()
        {
            throw new NotImplementedException();
        }
    } 
}