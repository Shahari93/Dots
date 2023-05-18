using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dots.GamePlay.Powerups;

namespace Dots.GamePlay.UI.PowerupsDisplay
{
    public class PowerupDisplay : MonoBehaviour
    {
        private bool shouldEnableImage;
        [SerializeField] Image powerupDurationImage;

        private void OnEnable()
        {
            powerupDurationImage.enabled = false;
            PowerupEffectSO.InvokePowerupUI += EnablePowerupDurationDisplay;
        }

        private IEnumerator EnablePowerupDurationCoroutine(float duration)
        {
            if (shouldEnableImage)
            {
                if (duration != 0)
                {
                    powerupDurationImage.enabled = true;
                }

                while (duration > 0)
                {
                    duration -= Time.deltaTime;
                    powerupDurationImage.fillAmount -= powerupDurationImage.fillAmount / duration * Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                if (duration <= 0)
                {
                    powerupDurationImage.fillAmount = 1;
                    powerupDurationImage.enabled = false;
                }
            }
        }

        private void EnablePowerupDurationDisplay(float duration)
        {
            shouldEnableImage = true;
            StartCoroutine(EnablePowerupDurationCoroutine(duration));
        }

        private void OnDisable()
        {
            PowerupEffectSO.InvokePowerupUI -= EnablePowerupDurationDisplay;
        }
    } 
}