using System;
using UnityEngine;
using UnityEngine.UI;
using Dots.Audio.Manager;

namespace Dots.Audio
{
    public class ToggleAudio : MonoBehaviour
    {
        [SerializeField] SpriteState spriteState;
        [SerializeField] Button musicButton, sfxButton;

        public void MusicSpriteChange()
        {
            musicButton.spriteState = spriteState;
        }

        public void SFXSpriteChange()
        {
            sfxButton.spriteState = spriteState;
            AudioManager.Instance.ToggleSFX();
        }   
    }
}

//[SerializeField] Toggle musicToggle, sfxToggle;

//private void Start()
//{
//    if (PlayerPrefs.HasKey("MusicToggle") && PlayerPrefs.HasKey("SFXToggle"))
//    {
//        musicToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("MusicToggle"));
//        sfxToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("SFXToggle"));
//    }
//    else
//    {
//        musicToggle.isOn = true;
//        sfxToggle.isOn = true;
//    }
//}

//public void ToggleMusic()
//{
//    musicToggle.onValueChanged.AddListener(delegate
//    {
//        musicToggle.isOn = !musicToggle.isOn;
//    });
//    AudioManager.Instance.ToggleMusic();
//}

//public void ToggleSFX()
//{
//    sfxToggle.onValueChanged.AddListener(delegate
//    {
//        sfxToggle.isOn = !sfxToggle.isOn;
//    });
//    AudioManager.Instance.ToggleSFX();
//}

//private void OnDestroy()
//{
//    PlayerPrefs.SetInt("MusicToggle", Convert.ToInt32(musicToggle.isOn));
//    PlayerPrefs.SetInt("SFXToggle", Convert.ToInt32(sfxToggle.isOn));
//}