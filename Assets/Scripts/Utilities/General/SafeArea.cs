using UnityEngine;

namespace Dots.Utilities.SafeArea
{
	public class SafeArea : MonoBehaviour
	{
		RectTransform rectTransform;
		Rect safeArea;

		Vector2 minAnchor;
		Vector2 maxAnchor;

        void Awake()
        {
            SetSafeArea();
        }

        void SetSafeArea()
        {
            rectTransform = GetComponent<RectTransform>();
            safeArea = Screen.safeArea;

            minAnchor = safeArea.position;
            maxAnchor = minAnchor + safeArea.size;

            minAnchor.x /= Screen.width;
            minAnchor.y /= Screen.height;

            maxAnchor.x /= Screen.width;
            maxAnchor.y /= Screen.height;


            rectTransform.anchorMin = minAnchor;
            rectTransform.anchorMax = maxAnchor;
        }
    } 
}