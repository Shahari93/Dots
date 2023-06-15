using UnityEngine;

namespace Dots.Utilities.FPS
{
	public class SetFPS : MonoBehaviour
	{
#if UNITY_ANDROID || UNITY_IPONE
        void Awake()
        {
            Application.targetFrameRate = 60;
        } 
#endif
    } 
}