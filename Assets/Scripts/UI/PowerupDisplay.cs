using Dots.GamePlay.Powerups;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerupDisplay : MonoBehaviour
{
    [SerializeField] Image powerupDurationImage;

    private void OnEnable()
    {
        PowerupEffectSO.InvokePowerupUI += EnablePowerupDurationDisplay;
    }

    private void Awake()
    {
        powerupDurationImage.gameObject.SetActive(false);
        powerupDurationImage.fillAmount = 1;
    }

    private void EnablePowerupDurationDisplay(float duration)
    {
        if (duration > 0)
        {
            powerupDurationImage.gameObject.SetActive(true);
            StartCoroutine(StartPowerupDurationCoroutine(duration));
        }
    }

    private IEnumerator StartPowerupDurationCoroutine(float duration)
    {
        duration -= Time.deltaTime;
        while (duration > 0)
        {
            powerupDurationImage.fillAmount = 1 / duration * Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (duration <= 0)
            {
                powerupDurationImage.fillAmount = 1;
                powerupDurationImage.gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnDisable()
    {
        PowerupEffectSO.InvokePowerupUI += EnablePowerupDurationDisplay;
    }
}