using UnityEngine;
using Dots.Audio.Manager;
using UnityEngine.UI;
using System;

namespace Dots.Audio
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] Toggle musicButton, sfxButton;
        public void ToggleMusic()
        {
            AudioManager.Instance.ToggleMusic();
            musicButton.isOn = !musicButton.isOn;
            PlayerPrefs.SetInt("Music", Convert.ToInt32(musicButton.isOn));
        }

        public void ToggleSFX()
        {
            AudioManager.Instance.ToggleSFX();
            sfxButton.isOn = !sfxButton.isOn;
            PlayerPrefs.SetInt("SFX", Convert.ToInt32(sfxButton.isOn));
        }
    }
}