using System;
using UnityEngine;

namespace Dots.Audio.Manager
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] Sounds[] musicAudio, sfxAudio;
        [SerializeField] AudioSource musicSource, sfxSource;
        public static AudioManager Instance;

        private const string MUSIC_TOGGLE = "Music";
        private const string SOUNDS_TOGGLE = "SFX";
        
        #region Singleton
        void Awake()
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
        }
        #endregion

        void OnEnable()
        {
            if (PlayerPrefs.HasKey(MUSIC_TOGGLE) && PlayerPrefs.HasKey(SOUNDS_TOGGLE))
            {
                musicSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt(MUSIC_TOGGLE));
                sfxSource.mute = Convert.ToBoolean(PlayerPrefs.GetInt(SOUNDS_TOGGLE));
            }
            else
            {
                musicSource.mute = false;
                sfxSource.mute = false;
            }
        }

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
            PlayerPrefs.SetInt(MUSIC_TOGGLE, Convert.ToInt32(musicSource.mute));
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
            PlayerPrefs.SetInt(SOUNDS_TOGGLE, Convert.ToInt32(sfxSource.mute));
        }
        #endregion

        private void SaveDataOnExit()
        {
            PlayerPrefs.SetInt(SOUNDS_TOGGLE, Convert.ToInt32(sfxSource.mute));
            PlayerPrefs.SetInt(MUSIC_TOGGLE, Convert.ToInt32(musicSource.mute));
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
}