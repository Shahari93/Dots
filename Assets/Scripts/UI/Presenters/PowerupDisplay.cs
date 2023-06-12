using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Dots.GamePlay.Powerups;

namespace Dots.GamePlay.UI.PowerupsDisplay
{
    public class PowerupDisplay : MonoBehaviour
    {
        bool shouldEnableImage;
        [SerializeField] Image powerupDurationImage;

        void OnEnable()
        {
            powerupDurationImage.enabled = false;
            PowerupEffectSO.InvokePowerupUI += EnablePowerupDurationDisplay;
        }

        IEnumerator EnablePowerupDurationCoroutine(float duration)
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

        void EnablePowerupDurationDisplay(float duration)
        {
            shouldEnableImage = true;
            StartCoroutine(EnablePowerupDurationCoroutine(duration));
        }

        void OnDisable()
        {
            PowerupEffectSO.InvokePowerupUI -= EnablePowerupDurationDisplay;
        }
    } 
}