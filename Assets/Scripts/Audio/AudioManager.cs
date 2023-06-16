using System;
using UnityEngine;

namespace Dots.Audio.Manager
{
    /// <summary>
    /// This class controls which sound we play and setting the mute status of the music/SFX 
    /// </summary>
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
            // Checking if we have a saved status of mute for music and SFX. If not (First time opened) the sfx and music are not muted
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
        /// <summary>
        /// Call this method when you want to play the BG music
        /// </summary>
        /// <param name="name">
        /// the name of the Music file
        /// </param>
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
        /// <summary>
        /// Toggle the mute state for Music (Called from the settings menu presenter)
        /// </summary>
        public void ToggleMusic()
        {
            musicSource.mute = !musicSource.mute;
            PlayerPrefs.SetInt(MUSIC_TOGGLE, Convert.ToInt32(musicSource.mute));
        }
        #endregion

        #region SFX control
        /// <summary>
        /// Call this method when you want to play the SFX 
        /// </summary>
        /// <param name="name">the name of the SFX file</param>
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
        /// <summary>
        /// Toggle the mute state for SFX (Called from the settings menu presenter)
        /// </summary>
        public void ToggleSFX()
        {
            sfxSource.mute = !sfxSource.mute;
            PlayerPrefs.SetInt(SOUNDS_TOGGLE, Convert.ToInt32(sfxSource.mute));
        }
        #endregion
        /// <summary>
        /// Saving the data to PlayerPrefs
        /// </summary>
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