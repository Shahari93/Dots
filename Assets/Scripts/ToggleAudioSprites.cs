using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;

// TODO: Change class name to "SettingMenuManager" and make it more dynamic
public class ToggleAudioSprites : MonoBehaviour
{
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] Image musicTargetButton, sfxTargetButton;
    [SerializeField] bool isSoundOn = true, isSFXOn = true;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("MusicToggle") && PlayerPrefs.HasKey("SFXToggle"))
        {
            isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
            isSFXOn = Convert.ToBoolean(PlayerPrefs.GetInt("SFXToggle"));
        }
        else
        {
            isSoundOn = true;
            isSFXOn = true;
        }

        musicTargetButton.sprite = isSoundOn ? buttonSprites[0] : buttonSprites[1];
        sfxTargetButton.sprite = isSFXOn ? buttonSprites[0] : buttonSprites[1];
    }

    public void ChangeMusicButtonSprite()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        if (isSoundOn)
        {
            musicTargetButton.sprite = buttonSprites[1];
            AudioManager.Instance.ToggleMusic();
            isSoundOn = false;
            return;
        }
        else
        {
            musicTargetButton.sprite = buttonSprites[0];
            AudioManager.Instance.ToggleMusic();
            isSoundOn = true;
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
            return;
        }
        else
        {
            sfxTargetButton.sprite = buttonSprites[0];
            AudioManager.Instance.ToggleSFX();
            isSFXOn = true;
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(isSoundOn));
        PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(isSFXOn));
    }
}