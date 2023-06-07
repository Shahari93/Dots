using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;

// TODO: Change class name to "SettingMenuManager" and make it more dynamic
public class ToggleAudioSprites : MonoBehaviour
{
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] Image musicTargetButton, sfxTargetButton, hapticTargetButton;
    [SerializeField] bool isSoundOn = true, isSFXOn = true, isHapticOn = true;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicToggle") && PlayerPrefs.HasKey("SFXToggle") && PlayerPrefs.HasKey("HapticToggle"))
        {
            isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
            isSFXOn = Convert.ToBoolean(PlayerPrefs.GetInt("SFXToggle"));
            isHapticOn = Convert.ToBoolean(PlayerPrefs.GetInt("HapticToggle"));
        }
        else
        {
            isSoundOn = true;
            isSFXOn = true;
            isHapticOn = true;
        }

        musicTargetButton.sprite = isSoundOn ? buttonSprites[0] : buttonSprites[1];
        sfxTargetButton.sprite = isSFXOn ? buttonSprites[0] : buttonSprites[1];
        hapticTargetButton.sprite = isHapticOn ? buttonSprites[0] : buttonSprites[1];
    }

    public void ChangeMusicButtonSprite()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (isSoundOn)
        {
            musicTargetButton.sprite = buttonSprites[1];
            AudioManager.Instance.ToggleMusic();
            isSoundOn = false;
            PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(isSoundOn));
            return;
        }
        else
        {
            musicTargetButton.sprite = buttonSprites[0];
            AudioManager.Instance.ToggleMusic();
            isSoundOn = true;
            PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(isSoundOn));
        }
    }

    public void ChangeSFXButtonSprite()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (isSFXOn)
        {
            sfxTargetButton.sprite = buttonSprites[1];
            AudioManager.Instance.ToggleSFX();
            isSFXOn = false;
            PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(isSFXOn));
            return;
        }
        else
        {
            sfxTargetButton.sprite = buttonSprites[0];
            AudioManager.Instance.ToggleSFX();
            isSFXOn = true;
            PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(isSFXOn));
        }
    }

    public void ChangeHapticButtonSprite()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (isHapticOn)
        {
            hapticTargetButton.sprite = buttonSprites[1];
            isHapticOn = false;
            PlayerPrefs.SetInt("HapticToggle", Convert.ToInt32(isHapticOn));
            return;
        }
        else
        {
            hapticTargetButton.sprite = buttonSprites[0];
            isHapticOn = true;
            PlayerPrefs.SetInt("HapticToggle", Convert.ToInt32(isHapticOn));
        }
    }

    private void OnApplicationPause(bool pause)
    {
        PlayerPrefs.SetInt("HapticToggle", Convert.ToInt32(isHapticOn));
        PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(isSFXOn));
        PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(isSoundOn));
        PlayerPrefs.Save();
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt("HapticToggle", Convert.ToInt32(isHapticOn));
        PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(isSFXOn));
        PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(isSoundOn));
        PlayerPrefs.Save();
    }
}