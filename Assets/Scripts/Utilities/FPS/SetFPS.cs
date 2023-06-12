using UnityEngine;

namespace Dots.Utils.FPS
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