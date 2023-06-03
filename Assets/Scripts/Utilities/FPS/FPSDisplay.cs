using TMPro;
using UnityEngine;

namespace Dots.Utils.FPS
{
    /// <summary>
    /// Keeping track of multiple frames and calculate the avarage between them to show FPS
    /// </summary>
    public class FPSDisplay : MonoBehaviour
    {
        private int lastFrameIndex;
        float[] frameDeltaTimeArray;

        [SerializeField] TMP_Text fpsText;

        void Awake()
        {
            frameDeltaTimeArray = new float[50];
        }

        void Update()
        {
            frameDeltaTimeArray[lastFrameIndex] = Time.deltaTime;
            lastFrameIndex = (lastFrameIndex + 1) % frameDeltaTimeArray.Length;

            fpsText.text = Mathf.RoundToInt(CalculateFPS()).ToString();
        }

        private float CalculateFPS()
        {
            float total = 0f;
            foreach (float deltaTime in frameDeltaTimeArray)
            {
                total += deltaTime;
            }
            return frameDeltaTimeArray.Length / total;
        }
    }  
}