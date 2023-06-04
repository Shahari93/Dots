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
        }
        #endregion

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

        // Call this method when you want to play the SFX 
        public void PlaySFX(string name)
        {
            // Finding the right sfx clip from the array using the name of the clip
            Sounds sound = Array.Find(sfxAudio, sound => sound.audioName == name);
            if(sound == null)
            {
                Debug.Log("Sound not found");
            }
            else
            {
                sfxSource.PlayOneShot(sound.audioClip);
            }
        }
    }
}