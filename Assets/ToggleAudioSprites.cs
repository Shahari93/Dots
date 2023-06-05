using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;

public class ToggleAudioSprites : MonoBehaviour
{
    [SerializeField] Sprite[] buttonSprites;
    [SerializeField] Image musicTargetButton, sfxTargetButton;
    [SerializeField] bool isSoundOn = true, isSFXOn = true;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("Music") && PlayerPrefs.HasKey("SFX"))
        {
            isSoundOn = Convert.ToBoolean(PlayerPrefs.GetInt("Music"));
            isSFXOn = Convert.ToBoolean(PlayerPrefs.GetInt("SFX"));
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
        PlayerPrefs.SetInt("Music", Convert.ToInt32(isSoundOn));
        PlayerPrefs.SetInt("SFX", Convert.ToInt32(isSFXOn));
    }
}