using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Dots.Feature.KeyAndChest.Chest.Tap
{
    public class TapOnChest : MonoBehaviour
    {
        [SerializeField] GraphicRaycaster m_Raycast;
        [SerializeField] EventSystem m_EventSystem;
        [SerializeField] RectTransform canvasRect;
        PointerEventData m_PointerEventData;

        private void Update()
        {
            CheckIfPlayerTappedOnChest();
        }

        private void CheckIfPlayerTappedOnChest()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    m_PointerEventData = new PointerEventData(m_EventSystem);
                    m_PointerEventData.position = touch.position;
                    List<RaycastResult> results = new List<RaycastResult>();
                    m_Raycast.Raycast(m_PointerEventData, results);
                    if (results.Count > 0)
                    {
                        Debug.Log("Hit: " + results[0].gameObject.name);
                    }
                }
            }
        }
    }
}