using UnityEngine;
using UnityEngine.UI;
using Dots.GamePlay.Powerups;
using System;
using System.Collections;

public class PowerupDisplay : MonoBehaviour
{
    private bool shouldEnableImage;
    private float powerupDuration = 5;
    [SerializeField] Image powerupDurationImage;

    private void OnEnable()
    {
        powerupDurationImage.fillAmount = 1;
        PowerupEffectSO.InvokePowerupUI += EnablePowerupDurationDisplay;
    }

    private void Start()
    {
        StartCoroutine(EnablePowerupDurationCoroutine(powerupDuration));
    }

    private IEnumerator EnablePowerupDurationCoroutine(float duration)
    {
        if (shouldEnableImage)
        {
            powerupDurationImage.enabled = true;
            while (powerupDuration > 0)
            {
                powerupDuration -= Time.deltaTime;
                powerupDurationImage.fillAmount -= 1 / powerupDuration * Time.deltaTime;
            }
            yield return new WaitForEndOfFrame();
        }
    }

    private void EnablePowerupDurationDisplay(float duration)
    {
        shouldEnableImage = true;
    }

    private void OnDisable()
    {
        PowerupEffectSO.InvokePowerupUI -= EnablePowerupDurationDisplay;
    }
}