using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;

// TODO: Change class name to "SettingMenuManager" and make it more dynamic
public class ToggleAudioSprites : MonoBehaviour
{
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] Image musicTargetButton, sfxTargetButton, hapticTargetButton;
    bool isSoundOn = true, isSFXOn = true;
    static bool isHapticOn = true;
    public static bool IsHapticOn
    {
        get
        {
            return isHapticOn;
        }
    }

    private const string MUSIC_TOGGLE = "MusicToggle";
    private const string SOUNDS_TOGGLE = "SFXToggle";
    private const string HAPTIC_TOGGLE = "HapticToggle";

    void OnEnable()
    {
        if (PlayerPrefs.HasKey(MUSIC_TOGGLE) && PlayerPrefs.HasKey(SOUNDS_TOGGLE) && PlayerPrefs.HasKey(HAPTIC_TOGGLE))
        {
            isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt(MUSIC_TOGGLE));
            isSFXOn = Convert.ToBoolean(PlayerPrefs.GetInt(SOUNDS_TOGGLE));
            isHapticOn = Convert.ToBoolean(PlayerPrefs.GetInt(HAPTIC_TOGGLE));
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

    void ChangeButtonSprite(ref bool isButtonOn, ref Image buttonImage, string buttonName)
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (isButtonOn)
        {
            buttonImage.sprite = buttonSprites[1];
            isButtonOn = false;
            PlayerPrefs.SetInt(buttonName, Convert.ToInt32(isButtonOn));
            return;
        }
        else
        {
            buttonImage.sprite = buttonSprites[0];
            isButtonOn = true;
            PlayerPrefs.SetInt(buttonName, Convert.ToInt32(isButtonOn));
        }
    }

    public void ChangeMusicButtonSprite()
    {
        ChangeButtonSprite(ref isSoundOn, ref musicTargetButton, MUSIC_TOGGLE);
        AudioManager.Instance.ToggleMusic();
    }

    public void ChangeSFXButtonSprite()
    {
        ChangeButtonSprite(ref isSFXOn, ref sfxTargetButton, SOUNDS_TOGGLE);
        AudioManager.Instance.ToggleSFX();
    }

    public void ChangeHapticButtonSprite()
    {
        ChangeButtonSprite(ref isHapticOn, ref hapticTargetButton, HAPTIC_TOGGLE);
    }

    void SaveDataOnExit()
    {
        PlayerPrefs.SetInt(HAPTIC_TOGGLE, Convert.ToInt32(isHapticOn));
        PlayerPrefs.SetInt(SOUNDS_TOGGLE, Convert.ToInt32(isSFXOn));
        PlayerPrefs.SetInt(MUSIC_TOGGLE, Convert.ToInt32(isSoundOn));
        PlayerPrefs.Save();
    }

    void OnApplicationPause(bool pause)
    {
        SaveDataOnExit();
    }

    void OnDestroy()
    {
        SaveDataOnExit();
    }
}