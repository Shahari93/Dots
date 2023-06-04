using System;
using UnityEngine;

namespace Dots.Audio.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] Sounds[] musicAudio, sfxAudio;
        [SerializeField] AudioSource musicSource, sfxSource;
        #region Singleton
        public static AudioManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            musicSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("Music"));
            sfxSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt("SFX"));
        }
        #endregion

        #region Music control
        // Call this method when you want to play the BG music
        public void PlayMusic(string name)
        {
            // Finding the right music clip from the array using the name of the clip
            Sounds sound = Array.Find(musicAudio, sound => sound.audioName == name);
            if (sound == null)
            {
                Debug.Log("Sound not found");
            }
            else
            {
                musicSource.clip = sound.audioClip;
                musicSource.Play();
            }
        }

        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
            PlayerPrefs.SetInt("Music", Convert.ToInt32(musicSource.mute));
        } 
        #endregion

        #region SFX control
        // Call this method when you want to play the SFX 
        public void PlaySFX(string name)
        {
            // Finding the right sfx clip from the array using the name of the clip
            Sounds sound = Array.Find(sfxAudio, sound => sound.audioName == name);
            if (sound == null)
            {
                Debug.Log("Sound not found");
            }
            else
            {
                sfxSource.PlayOneShot(sound.audioClip);
            }
        }

        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
            PlayerPrefs.SetInt("SFX", Convert.ToInt32(sfxSource.mute));
        } 
        #endregion
    }
}