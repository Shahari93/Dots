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

        private void OnEnable()
        {
            if (!PlayerPrefs.HasKey("MusicToggle") && !PlayerPrefs.HasKey("SFXToggle"))
            {
                PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(true));
                PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(true));

                musicToggleState = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
                sfxToggleState = Convert.ToBoolean(PlayerPrefs.GetInt("SFXToggle"));
            }

            else
            {
                musicToggleState = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
                sfxToggleState = Convert.ToBoolean(PlayerPrefs.GetInt("SFXToggle"));
            }

            musicToggle.isOn = musicToggleState;
            sfxToggle.isOn = sfxToggleState;
        }

        public void ToggleMusic()
        {
            AudioManager.Instance.ToggleMusic();
        }

        public void ToggleSFX()
        {
            AudioManager.Instance.ToggleSFX();
        }

        private void OnDisable()
        {
            PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(musicToggle.isOn));
            PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(sfxToggle.isOn));
        }
    }
}