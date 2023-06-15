using System;
using UnityEngine;

namespace Dots.Audio
{
    /// <summary>
    /// This class holds the data of each audio clip we want to use
    /// The audioName string field is for using the PlayMusic and PlaySFX functions
    /// </summary>
    [Serializable]
	public class Sounds
	{
		public string audioName;
		public AudioClip audioClip;
	} 
}