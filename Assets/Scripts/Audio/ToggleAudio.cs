using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;

namespace Dots.Audio
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] Toggle musicToggle, sfxToggle;
        private bool musicToggleState, sfxToggleState;

        private void Start()
        {
            if (PlayerPrefs.HasKey("MusicToggle"))
            {
                musicToggleState = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
                musicToggle.isOn = musicToggleState;
            }
            else
            {
                musicToggleState = true;
                musicToggle.isOn = true;
            }
        }

        public void ToggleMusic()
        {
            AudioManager.Instance.ToggleMusic();
            musicToggleState = musicToggle.isOn;
            PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(musicToggleState));
        }

        public void ToggleSFX()
        {
            AudioManager.Instance.ToggleSFX();
            sfxToggleState = sfxToggle.isOn;
            PlayerPrefs.SetInt("SFX", Convert.ToInt32(sfxToggleState));
        }
    }
}